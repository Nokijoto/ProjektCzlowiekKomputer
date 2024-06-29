using Microsoft.AspNetCore.Mvc;
using Project.CrossCutting.Dtos.CreateDto;
using ProjektCzlowiekKomputer.Interfaces;

namespace ProjektCzlowiekKomputer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShelveController : ControllerBase
    {
        private readonly IShelveService _shelveService;
        public ShelveController(IShelveService shelveService)
        {
            _shelveService = shelveService;
        }
        [HttpGet]
        public async Task<IActionResult> GetShelves()
        {
            var result = await _shelveService.GetShelves();
            return Ok(result);
        }
        [HttpGet("guid")]
        public async Task<IActionResult> GetShelve(Guid shelveId)
        {
            var result = await _shelveService.GetShelveByGuid(shelveId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateShelve([FromBody] CreateShelveDto createShelveDto)
        {
            var result = await _shelveService.AddShelve(createShelveDto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteShelve(Guid shelveId)
        {
            var result = await _shelveService.DeleteShelve(shelveId);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateShelve([FromBody] UpdateShelveDto updateShelveDto)
        {
            var result = await _shelveService.UpdateShelve(updateShelveDto);
            return Ok(result);
        }
        [HttpDelete]
        [Route("RemoveBookFromShelve")]
        public async Task<IActionResult> RemoveBookFromShelve(Guid bookGuid, Guid shelveGuid)
        {
            var result = await _shelveService.RemoveBookFromShelve(shelveGuid, bookGuid);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetShelvesBooks")]
        public async Task<IActionResult> GetShelvesBooks(Guid shelveGuid)
        {
            var result = await _shelveService.GetBooksFromShelve(shelveGuid);
            return Ok(result);
        }
        [HttpPost]
        [Route("AddBooksToShelve")]
        public async Task<IActionResult> AddBooksToShelve(Guid bookGuid, Guid shelveGuid)
        {
            var result = await _shelveService.AddBookToShelve(shelveGuid,bookGuid);
            return Ok(result);
        }



    }
}
