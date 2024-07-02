using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CrossCutting.Dtos.CreateDto;
using ProjektCzlowiekKomputer.Interfaces;

namespace ProjektCzlowiekKomputer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthorController : ControllerBase
    {
      private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuthors()
        {
            var result = await _authorService.GetAuthorsAsync();
            return Ok(result);
        }
        [HttpGet("guid")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuthor(Guid authorId)
        {
            var result = await _authorService.GetAuthorAsync(authorId);
            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto createAuthorDto)
        {
            var result = await _authorService.CreateAuthorAsync(createAuthorDto);
            return Ok(result);
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteAuthor(Guid authorId)
        {
            var result = await _authorService.DeleteAuthorAsync(authorId);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorDto updateAuthorDto)
        {
            var result = await _authorService.UpdateAuthorAsync(updateAuthorDto);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddAuthorsBooks")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAuthorsBooks(Guid bookGuid, Guid authorGuid)
        {
            var result = await _authorService.AddAuthorsBooksAsync(bookGuid, authorGuid);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAuthorsBooks")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuthorsBooks(Guid authorGuid)
        {
            var result = await _authorService.GetAuthorsBooksAsync(authorGuid);
            return Ok(result);
        }

    }
}
