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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _category;

        public CategoryController(CategoryService category)
        {
            _category = category;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _category.GetAll();
            return Ok(result);
        }

        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult> GetAllById(Guid Id)
        {
            var result = await _category.GetById(Id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CreateOrUpdateCategory(CreateOrUpdateCategoryDto Input)
        {
            var result = await _category.CreateOrEdit(Input);
            return Ok(result);
        }

        [HttpPut("[action]/{Id}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var result = await _category.Delete(Id);
            return Ok(result);
        }
    }
}
