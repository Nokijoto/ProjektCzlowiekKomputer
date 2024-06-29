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
    public class ShelveService : IShelveService
    {
        private readonly ProjectDbContext _db;

        private readonly IMapper _mapper;

        public ShelveService(IMapper mapper, ProjectDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<CrudOperationResult<BookShelvesDto>> AddBookToShelve(Guid shelveGuid, Guid bookGuid)
        {
            
            try
            {
                
                var book = await _db.Books.SingleOrDefaultAsync(x => x.Guid == bookGuid);
                var shelve = await _db.Shelves.SingleOrDefaultAsync(x => x.Guid == shelveGuid);
                if (book == null || shelve == null)
                {
                    return new CrudOperationResult<BookShelvesDto>()
                    {
                        Message = $"Book or Shelve not found. Book Guid: {(book == null ? "null" : bookGuid.ToString())}, Shelve Guid: {(shelve == null ? "null" : shelveGuid.ToString())}",
                        Status = CrudOperationResultStatus.Failure,
                        Result = null
                    };
                }
                var existingConnection = await _db.BookShelves.SingleOrDefaultAsync(ba => ba.Guid == bookGuid && ba.Guid == shelveGuid);
                if (existingConnection != null)
                {
                    return new CrudOperationResult<BookShelvesDto>()
                    {
                        Status = CrudOperationResultStatus.Failure,
                        Result = null,
                        Message = "The connection between the book and shelves already exists."
                    };
                }

                var bookShelve = new BookShelves()
                {
                    Book = book,
                    Shelves = shelve,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedBy = "API",
                    UpdatedBy = null,
                    Guid = Guid.NewGuid()
                };

                await _db.BookShelves.AddAsync(bookShelve);
                await _db.SaveChangesAsync();
                var newBookShelve = await _db.BookShelves.FirstOrDefaultAsync(x => x.Guid == bookShelve.Guid);
                return new CrudOperationResult<BookShelvesDto>()
                {
                    Message = "Book added to shelve successfully",
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookShelvesDto>(newBookShelve)
                };
            }
            catch (Exception e)
            {
                var errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                return new CrudOperationResult<BookShelvesDto>()
                {
                    Message = $"Book add failure Error: {errorMessage}",
                    Status = CrudOperationResultStatus.Failure,
                    Result = null
                };
            }
        }

        public async Task<CrudOperationResult<ShelveDto>> AddShelve(CreateShelveDto shelveDto, Guid userGuid)
        {
            try
            {
                var user = await _db.Users.SingleOrDefaultAsync(x => x.UserGuid == userGuid);



                var shelve = _mapper.Map<Shelves>(shelveDto);
                shelve.UpdatedBy = null;
                shelve.CreatedBy = "API";
                shelve.CreatedAt = DateTime.Now;
                shelve.UpdatedAt = DateTime.Now;
                shelve.Guid = Guid.NewGuid();

                await _db.Shelves.AddAsync(shelve);
                await _db.SaveChangesAsync();

                var newShelve = await _db.Shelves.FirstOrDefaultAsync(x => x.Guid == shelve.Guid);
                var userShelve= new UserShelves()
                {
                    Shelves = newShelve,
                    User = user,
                    UserGuid = user.UserGuid,
                    ShelvesGuid = newShelve.Guid,
                    ShelvesId = newShelve.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedBy = "API",
                    UpdatedBy = null,
                    Guid = Guid.NewGuid()
                };

                return new CrudOperationResult<ShelveDto>()
                {
                    Message = "Shelve added successfully",
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<ShelveDto>(newShelve)
                };
            }
            catch (Exception e)
            {
                return new CrudOperationResult<ShelveDto>()
                {
                    Message = $"Shelve add failure Error: {e.Message}",
                    Status = CrudOperationResultStatus.Failure,
                    Result = null
                };
            }
        }

        public async Task<CrudOperationResult<ShelveDto>> DeleteShelve(Guid guid)
        {
            try
            {
               _db.Shelves.Remove(await _db.Shelves.FirstOrDefaultAsync(x => x.Guid == guid));
                await _db.SaveChangesAsync();
                return new CrudOperationResult<ShelveDto>()
                {
                    Message = "Shelve deleted successfully",
                    Status = CrudOperationResultStatus.Success,
                    Result = null
                };

            }
            catch (Exception e)
            {
                return new CrudOperationResult<ShelveDto>()
                {
                    Message = $"Shelve delete failure Error: {e.Message}",
                    Status = CrudOperationResultStatus.Failure,
                    Result = null
                };
            }
        }

        public async Task<CrudOperationResult<List<BookDto>>> GetBooksFromShelve(Guid shelveGuid)
        {
            try
            {
                var booksShelves = await _db.BookShelves.Where(x => x.Shelves.Guid == shelveGuid).Select(ba => ba.Book).ToListAsync();
                if(!booksShelves.Any())
                {
                    return new CrudOperationResult<List<BookDto>>()
                    {
                        Message = "No books found in shelve",
                        Status = CrudOperationResultStatus.Failure,
                        Result = null
                    };
                }
                var books = _mapper.Map<List<BookDto>>(booksShelves);
                return new CrudOperationResult<List<BookDto>>()
                {
                    Message = "Books fetched successfully",
                    Status = CrudOperationResultStatus.Success,
                    Result = books
                };
            }
            catch (Exception e)
            {
                return new CrudOperationResult<List<BookDto>>()
                {
                    Message = $"Books fetch failure Error: {e.Message}",
                    Status = CrudOperationResultStatus.Failure,
                    Result = null
                };
            }
        }

        public async Task<CrudOperationResult<ShelveDto>> GetShelveByGuid(Guid guid)
        {
            try
            {
                var shelve = await _db.Shelves.FirstOrDefaultAsync(x => x.Guid == guid);
                var shelveDto = _mapper.Map<ShelveDto>(shelve);
                return new CrudOperationResult<ShelveDto>()
                {
                    Message = "Shelve fetched successfully",
                    Status = CrudOperationResultStatus.Success,
                    Result = shelveDto
                };
            }
            catch (Exception e)
            {
                return new CrudOperationResult<ShelveDto>()
                {
                    Message = $"Shelve feched failure Error: {e.Message}",
                    Status = CrudOperationResultStatus.Failure,
                    Result = null
                };
            }
        }

        public async Task<CrudOperationResult<ShelveDto>> GetShelveById(int id)
        {
            try
            {
                var shelve = await _db.Shelves.FirstOrDefaultAsync(x => x.Id == id);
                var shelveDto = _mapper.Map<ShelveDto>(shelve);
                return new CrudOperationResult<ShelveDto>()
                {
                    Message = "Shelve fetched successfully",
                    Status = CrudOperationResultStatus.Success,
                    Result = shelveDto
                };
            }
            catch (Exception e)
            {
                return new CrudOperationResult<ShelveDto>()
                {
                    Message = $"Shelve feched failure Error: {e.Message}",
                    Status = CrudOperationResultStatus.Failure,
                    Result = null
                };
            }
        }

        public async Task<CrudOperationResult<List<ShelveDto>>> GetShelves()
        {
           
            try
            {
                var shelves = await _db.Shelves.ToListAsync();
                var shelvesDto = _mapper.Map<List<ShelveDto>>(shelves);
                return new CrudOperationResult<List<ShelveDto>>()
                {
                    Message = "Shelves fetched successfully",
                    Status= CrudOperationResultStatus.Success,
                    Result = shelvesDto
                };

            }
            catch (Exception e)
            {
                return new CrudOperationResult<List<ShelveDto>>()
                {
                    Message =$"Shelves feched failure Error: {e.Message}" ,
                    Status = CrudOperationResultStatus.Failure,
                    Result = null
                };

            }
        }

        public async Task<CrudOperationResult<BookShelvesDto>> RemoveBookFromShelve(Guid shelveGuid, Guid bookGuid)
        {
            try
            {
                var bookShelve = await _db.BookShelves
                    .SingleOrDefaultAsync(bs => bs.Book.Guid == bookGuid && bs.Shelves.Guid == shelveGuid);

                if (bookShelve == null)
                {
                    return new CrudOperationResult<BookShelvesDto>()
                    {
                        Message = "Book or Shelve connection not found",
                        Status = CrudOperationResultStatus.Failure,
                        Result = null
                    };
                }

                _db.BookShelves.Remove(bookShelve);
                await _db.SaveChangesAsync();

                return new CrudOperationResult<BookShelvesDto>()
                {
                    Message = "Book removed from shelve successfully",
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookShelvesDto>(bookShelve)
                };
            }
            catch (Exception e)
            {
                var errorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
                return new CrudOperationResult<BookShelvesDto>()
                {
                    Message = $"Book remove failure Error: {errorMessage}",
                    Status = CrudOperationResultStatus.Failure,
                    Result = null
                };
            }
        }


        public async Task<CrudOperationResult<ShelveDto>> UpdateShelve(UpdateShelveDto shelveDto)
        {
            try
            {
                
                var shelve = await _db.Shelves.FirstOrDefaultAsync(x => x.Guid == shelveDto.guid);
                if (shelve == null)
                {
                    return new CrudOperationResult<ShelveDto>()
                    {
                        Message = "Shelve not found",
                        Status = CrudOperationResultStatus.Failure,
                        Result = null
                    };
                }
                shelve.UpdatedAt = DateTime.Now;
                shelve.UpdatedBy = "API";
                shelve.Name = shelveDto.Name;
                shelve.Description = shelveDto.Description;

                _db.Shelves.Update(shelve);
                await _db.SaveChangesAsync();
                var newShelve = await _db.Shelves.FirstOrDefaultAsync(x => x.Guid == shelve.Guid);
                return new CrudOperationResult<ShelveDto>()
                {
                    Message = "Shelve updated successfully",
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<ShelveDto>(newShelve)
                };
            }
            catch (Exception e)
            {
                return new CrudOperationResult<ShelveDto>()
                {
                    Message = $"Shelve update failure Error: {e.Message}",
                    Status = CrudOperationResultStatus.Failure,
                    Result = null
                };
            }
        }
    }
}
