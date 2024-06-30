using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Project.CrossCutting.Common;
using Project.CrossCutting.Dtos;
using Project.CrossCutting.Dtos.CreateDto;
using Project.Data;
using Project.Data.Entities;
using ProjektCzlowiekKomputer.Interfaces;
using System.Net;

namespace ProjektCzlowiekKomputer.Services
{
    public class BookService : IBookService
    {
        private readonly ProjectDbContext _db;
        private readonly IMapper _mapper;

        public BookService(ProjectDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CrudOperationResult<List<BookDto>>> GetBooksAsync()
        {
           var books = await _db.Books.ToListAsync();
           var bookDtos = _mapper.Map<List<BookDto>>(books);
           return new CrudOperationResult<List<BookDto>>()
           {
              Status = CrudOperationResultStatus.Success,
              Result = bookDtos,
              Message = "Books fetched successfully"
           };
        }


        public async Task<CrudOperationResult<BookDto>> AddBookAsync(CreateBookDto bookDto)
        {
            try
            {   var mappedBook = _mapper.Map<Book>(bookDto);
                mappedBook.Guid = Guid.NewGuid();
                mappedBook.CreatedBy = "API";
                mappedBook.CreatedAt = DateTime.Now;
                mappedBook.UpdatedBy = null;
                mappedBook.UpdatedAt = DateTime.Now;

                await _db.Books.AddAsync(mappedBook);
                await _db.SaveChangesAsync();
                var newItem = await _db.Books.FirstOrDefaultAsync(x => x.ISBN == bookDto.ISBN);
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookDto>(newItem),
                    Message = "Book added successfully"
                });
            }
            catch (Exception e)
            {
                if (e.InnerException != null) 
                {
                    return await Task.FromResult(new CrudOperationResult<BookDto>()
                    {
                        Status = CrudOperationResultStatus.Failure,
                        Result = null,
                        Message = $"Book addition failed  Error: {e.InnerException.Message}"
                    });
                }

                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Failure,
                    Result = null,
                    Message = $"Book addition failed Error: {e.Message}"
                });

            }
        }

        public async Task<CrudOperationResult<BookDto>> DeleteBookAsync(int bookId)
        {
            try
            {
                 _db.Books.Remove(await _db.Books.FirstOrDefaultAsync(x => x.Id == bookId));
                await _db.SaveChangesAsync();
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = null,
                    Message = "Book deleted successfully"
                });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Failure,
                    Result = null,
                    Message = $"Book deletion failed Error: {e.Message}"
                });
            }
        }


        public async Task<CrudOperationResult<BookDto>> GetBookByIdAsync(int bookId)
        {
            try
            {
                var item = await _db.Books.FirstOrDefaultAsync(x => x.Id == bookId);
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookDto>(item),
                    Message = "Book fetched successfully"
                });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.RecordNotFound,
                    Result = null,
                    Message = $"Book with id {bookId} not found Error: {e.Message}"
                });
            }
        }
        public async Task<CrudOperationResult<BookDto>> GetBookByAuthorAsync(int authorId)
        {
            try
            {
                var bookAuthor = await _db.BooksAuthors.FirstOrDefaultAsync(x => x.AuthorId == authorId);
                var item = await _db.Books.FirstOrDefaultAsync(x => x.Id == bookAuthor.BookId);
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookDto>(item),
                    Message = "Book fetched successfully"
                });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.RecordNotFound,
                    Result = null,
                    Message = $"Book with id {authorId} not found Error: {e.Message}"
                });
            }
        }

        public async Task<CrudOperationResult<BookDto>> GetBookByGenreAsync(string genre)
        {
            try
            {
                var item = await _db.Books.FirstOrDefaultAsync(x => x.Genre == genre);
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookDto>(item),
                    Message = "Book fetched successfully"
                });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.RecordNotFound,
                    Result = null,
                    Message = $"Book with id {genre} not foundn Error: {e.Message}"
                });
            }
        }

        public async Task<CrudOperationResult<BookDto>> GetBookByGuidAsync(string bookGuid)
        {
            try
            {

                var item = await _db.Books.FirstOrDefaultAsync(x => x.Guid == Guid.Parse(bookGuid));
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookDto>(item),
                    Message = "Book fetched successfully"
                });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.RecordNotFound,
                    Result = null,
                    Message = $"Book with id {bookGuid} not found Error: {e.Message}"
                });
            }
        }

       

        public async Task<CrudOperationResult<BookDto>> GetBookByIsbnAsync(string isbn)
        {
            try
            {
                var item = await _db.Books.FirstOrDefaultAsync(x => x.ISBN == isbn);
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookDto>(item),
                    Message = "Book fetched successfully"
                });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.RecordNotFound,
                    Result = null,
                    Message = $"Book with id {isbn} not found Error: {e.Message}"
                });
            }
        }

        public async Task<CrudOperationResult<BookDto>> GetBookByLanguageAsync(string language)
        {
            try
            {
                var item = await _db.Books.FirstOrDefaultAsync(x => x.Language == language);
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookDto>(item),
                    Message = "Book fetched successfully"
                });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.RecordNotFound,
                    Result = null,
                    Message = $"Book with id {language} not found Error: {e.Message}"
                });
            }
        }

        public async Task<CrudOperationResult<BookDto>> GetBookByPublisherAsync(string publisher)
        {
            try
            {
                var item = await _db.Books.FirstOrDefaultAsync(x => x.Publisher == publisher);
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookDto>(item),
                    Message = "Book fetched successfully"
                });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.RecordNotFound,
                    Result = null,
                    Message = $"Book with id {publisher} not found Error: {e.Message}"
                });
            }
        }

        public async Task<CrudOperationResult<BookDto>> GetBookByRatingAsync(int rating)
        {
            try
            {
                var item = await _db.Books.FirstOrDefaultAsync(x => x.Rating == rating);
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookDto>(item),
                    Message = "Book fetched successfully"
                });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.RecordNotFound,
                    Result = null,
                    Message = $"Book with id {rating} not found Error: {e.Message}"
                });
            }
        }

        public async Task<CrudOperationResult<BookDto>> GetBookByTitleAsync(string title)
        {
            try
            {
                var item = await _db.Books.FirstOrDefaultAsync(x => x.Title == title);
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Success,
                    Result = _mapper.Map<BookDto>(item),
                    Message = "Book fetched successfully"
                });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.RecordNotFound,
                    Result = null,
                    Message = $"Book with id {title} not found Error: {e.Message}"
                });
            }
        }


        public async Task<CrudOperationResult<BookDto>> UpdateBookAsync(UpdateBookDto bookDto, Guid guid)
        {
            try
            {
                var item = _db.Books.FirstOrDefault(x => x.Guid == guid);
                if (item == null)
                {
                    return await Task.FromResult(new CrudOperationResult<BookDto>()
                    {
                        Status = CrudOperationResultStatus.RecordNotFound,
                        Result = null,
                        Message = $"Book with id {bookDto.Id} not found"
                    });
                }
                else
                {
                    item.NumberOfPages = bookDto.NumberOfPages;
                    item.Title = bookDto.Title;
                    item.ISBN = bookDto.ISBN;
                    item.Genre = bookDto.Genre;
                    item.Language = bookDto.Language;
                    item.Publisher = bookDto.Publisher;
                    item.Rating = bookDto.Rating;
                    item.Format = bookDto.Format;
                    item.Description = bookDto.Description;
                    item.Edition = bookDto.Edition;
                    item.CoverImage = bookDto.CoverImage;
                    item.PublicationDate = bookDto.PublicationDate;
                    item.UpdatedBy = "API";
                    item.UpdatedAt = DateTime.Now;

                    _db.Books.Update(item);
                    await _db.SaveChangesAsync();

                    return await Task.FromResult(new CrudOperationResult<BookDto>()
                    {
                        Status = CrudOperationResultStatus.Success,
                        Result = _mapper.Map<BookDto>(item),
                        Message = "Book updated successfully"
                    });
                }
            }
            catch (Exception e)
            {
                return await Task.FromResult(new CrudOperationResult<BookDto>()
                {
                    Status = CrudOperationResultStatus.Failure,
                    Result = null,
                    Message = $"Book update failed Error: {e.Message}"
                });
            }
        }

        public async Task<CrudOperationResult<List<BookDto>>> GetBooksByFilterAsync(BookFilterDto filter)
        {
           throw new Exception("Not implemented yet");
        }
        

        
    }
}
