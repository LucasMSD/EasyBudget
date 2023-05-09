using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Errors;
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

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.Select(x => new { x.Message, x.Metadata }));
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDto createCategoryDto)
        {
            var result = await _categoryService.CreateAsync(createCategoryDto);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.Select(x => new { x.Message, x.Metadata }));
            }

            return CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var result = await _categoryService.UpdateAsync(updateCategoryDto);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.Select(x => new { x.Message, x.Metadata }));
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id, [FromBody] ReplaceCategoryDto deleteCategoryDto)
        {
            var result = await _categoryService.DeleteAsync(id, deleteCategoryDto);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.Select(x => new { x.Message, x.Metadata }));
            }

            return NoContent();
        }
    }
}
