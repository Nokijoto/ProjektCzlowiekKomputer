using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.CrossCutting.Dtos.CreateDto;
using Project.Data.Models;
using ProjektCzlowiekKomputer.Interfaces;

namespace ProjektCzlowiekKomputer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShelveController : ControllerBase
    {
        private readonly IShelveService _shelveService;
        private readonly UserManager<UserModel> _userManager;

        public ShelveController(IShelveService shelveService,UserManager<UserModel> userManager)
        {
            _shelveService = shelveService;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetShelves()
        {
            var result = await _shelveService.GetShelves();
            return Ok(result);
        }
        [HttpGet("guid")]
        [Authorize]
        public async Task<IActionResult> GetShelve(Guid shelveId)
        {
            var result = await _shelveService.GetShelveByGuid(shelveId);
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateShelve([FromBody] CreateShelveDto createShelveDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return BadRequest("User not found");
            }
            Guid userGuid = user.UserGuid;
            var result = await _shelveService.AddShelve(createShelveDto, userGuid);
            return Ok(result);
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteShelve(Guid shelveId)
        {
            var result = await _shelveService.DeleteShelve(shelveId);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateShelve([FromBody] UpdateShelveDto updateShelveDto)
        {
            var result = await _shelveService.UpdateShelve(updateShelveDto);
            return Ok(result);
        }
        [HttpDelete]
        [Route("RemoveBookFromShelve")]
        [Authorize]
        public async Task<IActionResult> RemoveBookFromShelve(Guid bookGuid, Guid shelveGuid)
        {
            var result = await _shelveService.RemoveBookFromShelve(shelveGuid, bookGuid);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetShelvesBooks")]
        [Authorize]
        public async Task<IActionResult> GetShelvesBooks(Guid shelveGuid)
        {
            var result = await _shelveService.GetBooksFromShelve(shelveGuid);
            return Ok(result);
        }
        [HttpPost]
        [Route("AddBooksToShelve")]
        [Authorize]
        public async Task<IActionResult> AddBooksToShelve(Guid bookGuid, Guid shelveGuid)
        {
            var result = await _shelveService.AddBookToShelve(shelveGuid,bookGuid);
            return Ok(result);
        }



    }
}
