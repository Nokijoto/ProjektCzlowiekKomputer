using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CrossCutting.Dtos;
using Project.CrossCutting.Dtos.CreateDto;
using ProjektCzlowiekKomputer.Interfaces;
using ProjektCzlowiekKomputer.Services;
using System;

namespace ProjektCzlowiekKomputer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookService.GetBooksAsync();
            return Ok(result);
        }

        [HttpGet("id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _bookService.GetBookByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("guid")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookByGuid(string guid)
        {
            var result = await _bookService.GetBookByGuidAsync(guid);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDto bookDto)
        {
            var result = await _bookService.AddBookAsync(bookDto);
            return Ok(result);
        }

        [HttpPut("guid")]
        [Authorize]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto bookDto, Guid guid)
        {
            var result = await _bookService.UpdateBookAsync(bookDto, guid);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            return Ok(result);
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBooksByFilter([FromQuery] BookFilterDto filter)
        {
            var result = await _bookService.GetBooksByFilterAsync(filter);
            return Ok(result);
        }

    }
}
