using Project.CrossCutting.Common;
using Project.CrossCutting.Dtos;
using Project.CrossCutting.Dtos.CreateDto;

namespace ProjektCzlowiekKomputer.Interfaces
{
    public interface IShelveService
    {
        Task<CrudOperationResult<ShelveDto>> AddShelve(CreateShelveDto shelveDto,Guid userGuid);
        Task<CrudOperationResult<ShelveDto>> UpdateShelve(UpdateShelveDto shelveDto);
        Task<CrudOperationResult<ShelveDto>> DeleteShelve(Guid id); 
        Task<CrudOperationResult<ShelveDto>> GetShelveById(int id);
        Task<CrudOperationResult<ShelveDto>> GetShelveByGuid(Guid guid);
        Task<CrudOperationResult<List<ShelveDto>>> GetShelves();
        Task<CrudOperationResult<BookShelvesDto>> AddBookToShelve(Guid shelveGuid,Guid bookGuid);
        Task<CrudOperationResult<BookShelvesDto>> RemoveBookFromShelve(Guid shelveGuid, Guid bookGuid);
        Task<CrudOperationResult<List<BookDto>>> GetBooksFromShelve(Guid shelveGuid);
    }
}
