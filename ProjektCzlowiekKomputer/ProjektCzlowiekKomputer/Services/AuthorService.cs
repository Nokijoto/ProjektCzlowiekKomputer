using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.CrossCutting.Common;
using Project.CrossCutting.Dtos;
using Project.CrossCutting.Dtos.CreateDto;
using Project.Data;
using Project.Data.Entities;
using ProjektCzlowiekKomputer.Interfaces;

namespace ProjektCzlowiekKomputer.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ProjectDbContext _db;
        private readonly IMapper _mapper;
        public AuthorService(IMapper mapper,ProjectDbContext db) {
            _mapper = mapper;
            _db = db;
        }
        public async Task<CrudOperationResult<AuthorDto>> CreateAuthorAsync(CreateAuthorDto createAuthorDto)
        {
            try
            {
                var mappedAuthor = _mapper.Map<Author>(createAuthorDto);
                mappedAuthor.Guid = Guid.NewGuid();
                mappedAuthor.CreatedBy = "API";
                mappedAuthor.CreatedAt = DateTime.Now;
                mappedAuthor.UpdatedBy = null;
                mappedAuthor.UpdatedAt = DateTime.Now;
                await _db.Authors.AddAsync(mappedAuthor);
                await _db.SaveChangesAsync();
                var newAuthor = await _db.Authors.FirstOrDefaultAsync(x => x.Name == createAuthorDto.Name); 
                return new CrudOperationResult<AuthorDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Message = "Author created successfully",
                    Result = _mapper.Map<AuthorDto>(newAuthor)
                };


            }
            catch (Exception e)
            {
                return new CrudOperationResult<AuthorDto>()
                {
                    Status = CrudOperationResultStatus.Failure,
                    Message = $"An error occurred while creating author {e.Message}",
                    Result = null,

                };
            }
        }

        public async Task<CrudOperationResult<AuthorDto>> DeleteAuthorAsync(Guid authorId)
        {
            try
            {
                 _db.Authors.Remove(_db.Authors.FirstOrDefault(x => x.Guid == authorId));
                await _db.SaveChangesAsync();
                return new CrudOperationResult<AuthorDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Message = "Author deleted successfully",
                    Result = null
                };
            }
            catch (Exception e)
            {
                return new CrudOperationResult<AuthorDto>()
                {
                    Status = CrudOperationResultStatus.Failure,
                    Message = $"An error occurred while deleting author {e.Message}",
                    Result = null,

                };
            }
        }

        public async Task<CrudOperationResult<AuthorDto>> GetAuthorAsync(Guid authorId)
        {
            try
            {
                var author = await _db.Authors.FirstOrDefaultAsync(x => x.Guid == authorId);
                return new CrudOperationResult<AuthorDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Message = "Author fetched successfully",
                    Result = _mapper.Map<AuthorDto>(author)
                };
            }
            catch (Exception e)
            {
                return new CrudOperationResult<AuthorDto>()
                {
                    Status = CrudOperationResultStatus.Failure,
                    Message = $"An error occurred while fetching author {e.Message}",
                    Result = null,

                };
            }
        }

        public async Task<CrudOperationResult<List<AuthorDto>>> GetAuthorsAsync()
        {
            try
            {
                var authors = await _db.Authors.ToListAsync();
                return new CrudOperationResult<List<AuthorDto>>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Message = "Authors fetched successfully",
                    Result = _mapper.Map<List<AuthorDto>>(authors)
                };
            }
            catch (Exception e)
            {
                return new CrudOperationResult<List<AuthorDto>>()
                {
                    Status = CrudOperationResultStatus.Failure,
                    Message = $"An error occurred while fetching authors {e.Message}",
                    Result = null,
                    
                };
            }
        }

        public async Task<CrudOperationResult<AuthorDto>> UpdateAuthorAsync(UpdateAuthorDto updateAuthorDto)
        {
            try
            {
                var author = await _db.Authors.FirstOrDefaultAsync(x => x.Guid == updateAuthorDto.Guid);
                if (author == null)
                {
                    return new CrudOperationResult<AuthorDto>()
                    {
                        Status = CrudOperationResultStatus.RecordNotFound,
                        Message = $"Author with id {updateAuthorDto.Guid} not found",
                        Result = null
                    };
                }
                author.Name = updateAuthorDto.Name;
                author.Surname = updateAuthorDto.Surname;
                author.Description = updateAuthorDto.Description;
                author.ImageUrl = updateAuthorDto.ImageUrl;
                author.Country = updateAuthorDto.Country;
                author.About = updateAuthorDto.About;

                author.UpdatedBy = "API";
                author.UpdatedAt = DateTime.Now;
                _db.Authors.Update(author);
                await _db.SaveChangesAsync();
                return new CrudOperationResult<AuthorDto>()
                {
                    Status = CrudOperationResultStatus.RecordNotFound,
                    Message = "Author updated successfully ",
                    Result = _mapper.Map<AuthorDto>(author)
                };

            }
            catch (Exception e)
            {
                return new CrudOperationResult<AuthorDto>()
                {
                    Status = CrudOperationResultStatus.Failure,
                    Message = $"An error occurred while updating author {e.Message}",
                    Result = null,

                };
            }
        }
    }
}
