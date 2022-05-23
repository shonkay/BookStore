using BookStore.Core;
using BookStore.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _book;

        public BookController(BookService book)
        {
            _book = book;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _book.GetAll();
            return Ok(result);
        }

        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult> GetAllById(Guid Id)
        {
            var result = await _book.GetById(Id);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllFavoriteBooks()
        {
            var result = await _book.GetAllFavoriteBooks();
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CreateOrUpdateBookStore(CreateOrUpdateBookDto Input)
        {
            var result = await _book.CreateOrEdit(Input);
            return Ok(result);
        }

        [HttpPost("[action]/{Id}")]
        public async Task<ActionResult> LendBook(Guid Id)
        {
            var result = await _book.LendBook(Id);
            return Ok(result);
        }

        [HttpPut("[action]/{Id}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var result = await _book.Delete(Id);
            return Ok(result);
        }
    }
}
