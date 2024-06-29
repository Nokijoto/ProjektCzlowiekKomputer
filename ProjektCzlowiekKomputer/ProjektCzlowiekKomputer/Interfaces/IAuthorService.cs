using Project.CrossCutting.Common;
using Project.CrossCutting.Dtos;
using Project.CrossCutting.Dtos.CreateDto;

namespace ProjektCzlowiekKomputer.Interfaces
{
    public interface IAuthorService
    {
        Task<CrudOperationResult<AuthorDto>> CreateAuthorAsync(CreateAuthorDto createAuthorDto);
        Task<CrudOperationResult<AuthorDto>> UpdateAuthorAsync(UpdateAuthorDto updateAuthorDto);
        Task<CrudOperationResult<AuthorDto>> DeleteAuthorAsync(Guid authorId);
        Task<CrudOperationResult<AuthorDto>> GetAuthorAsync(Guid authorId);
        Task<CrudOperationResult<List<AuthorDto>>> GetAuthorsAsync();
    }
}
