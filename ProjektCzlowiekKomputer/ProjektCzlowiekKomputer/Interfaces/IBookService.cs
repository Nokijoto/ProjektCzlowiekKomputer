using Project.CrossCutting.Common;
using Project.CrossCutting.Dtos;
using Project.CrossCutting.Dtos.CreateDto;

namespace ProjektCzlowiekKomputer.Interfaces
{
    public interface IBookService
    {
        public Task<CrudOperationResult<List<BookDto>>> GetBooksAsync();
        public Task<CrudOperationResult<BookDto>> AddBookAsync(CreateBookDto bookDto);
        public Task<CrudOperationResult<BookDto>> UpdateBookAsync(UpdateBookDto bookDto,Guid guid);
        public Task<CrudOperationResult<BookDto>> DeleteBookAsync(int bookId);
        public Task<CrudOperationResult<BookDto>> GetBookByIdAsync(int bookId);
        public Task<CrudOperationResult<BookDto>> GetBookByGuidAsync(string bookGuid);
        public Task<CrudOperationResult<BookDto>> GetBookByIsbnAsync(string isbn);
        public Task<CrudOperationResult<BookDto>> GetBookByTitleAsync(string title);
        public Task<CrudOperationResult<BookDto>> GetBookByAuthorAsync(int authorId);
        public Task<CrudOperationResult<BookDto>> GetBookByGenreAsync(string genre);
        public Task<CrudOperationResult<BookDto>> GetBookByPublisherAsync(string publisher);
        public Task<CrudOperationResult<BookDto>> GetBookByLanguageAsync(string language);
        public Task<CrudOperationResult<BookDto>> GetBookByRatingAsync(int rating);
        public Task<CrudOperationResult<List<BookDto>>> GetBooksByFilterAsync(BookFilterDto filter);

        
    }
}
