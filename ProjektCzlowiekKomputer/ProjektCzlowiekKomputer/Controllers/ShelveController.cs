using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.CrossCutting.Common;
using Project.CrossCutting.Dtos.CreateDto;
using Project.Data.Models;
using ProjektCzlowiekKomputer.Interfaces;
using System.Security.Claims;

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
        [HttpGet("admin/GetShelves")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetShelves()
        {
            var result = await _shelveService.GetShelves();
            return Ok(result);
        }

        //[HttpGet("admin/Shelve/{shelveId:guid}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> GetUserShelve(Guid shelveId)
        //{
        //    var result = await _shelveService.GetUserShelves(shelveId);
        //    return Ok(result);
        //}

        [HttpGet("/user/getmyshelves")]
        [Authorize]
        public async Task<IActionResult> GetMyShelves()
        {
            var userGuidClaim = User.FindFirst("userGuid");
            var userGuid = Guid.Parse(userGuidClaim.Value);
            var result = await _shelveService.GetUserShelves(userGuid);
            return Ok(result);
        }

        [HttpGet("user/{shelveId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetShelve(Guid shelveId)
        {
            var result = await _shelveService.GetShelveByGuid(shelveId);
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateShelve([FromBody] CreateShelveDto createShelveDto)
        {
            var userGuidClaim = User.FindFirst("userGuid");
            var userGuid = Guid.Parse(userGuidClaim.Value);
            var result = await _shelveService.AddShelve(createShelveDto, userGuid);

            if (result.Status == CrudOperationResultStatus.Failure)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Result);
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteShelve(Guid shelveId)
        {
            var userGuidClaim = User.FindFirst("userGuid");
            var userGuid = Guid.Parse(userGuidClaim.Value);
            var result = await _shelveService.DeleteShelve(shelveId, userGuid);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateShelve([FromBody] UpdateShelveDto updateShelveDto)
        {
            var userGuidClaim = User.FindFirst("userGuid");
            var userGuid = Guid.Parse(userGuidClaim.Value);
            var result = await _shelveService.UpdateShelve(updateShelveDto, userGuid);
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
