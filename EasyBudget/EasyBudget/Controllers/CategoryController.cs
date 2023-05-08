using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EasyBudget.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetAllAsync();

            if (result.IsFailed)
            {
                return NoContent();
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (result.IsFailed)
            {
                return NoContent();
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDto createCategoryDto)
        {
            var result = await _categoryService.CreateAsync(createCategoryDto);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var result = await _categoryService.UpdateAsync(updateCategoryDto);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok();
        }
    }
}
