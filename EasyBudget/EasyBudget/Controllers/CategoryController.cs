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

        /// <summary>
        /// Gets all categories in the system
        /// </summary>
        /// <returns code="200">Returns all categories in the system</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetAllAsync();

            return Ok(result.Value);
        }

        /// <summary>
        /// Gets a category with the specified id
        /// </summary>
        /// <returns code="200">Return the category with the specified id</returns>
        /// <returns code="400">The specified id is invalid</returns>
        /// <returns code="404">Category not found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.OfType<IBadRequestError>().Select(x => new { x.Message, x.Metadata }));
            }

            if (result.HasError<INotFoundError>())
            {
                return NotFound(result.Errors.OfType<INotFoundError>().Select(x => new { x.Message, x.Metadata }));
            }

            return Ok(result.Value);
        }


        /// <summary>
        /// Create a new category
        /// </summary>
        /// <returns code="201">Category created successfully</returns>
        /// <returns code="400">Unable to create the category due to validaton error</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDto createCategoryDto)
        {
            var result = await _categoryService.CreateAsync(createCategoryDto);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.OfType<IBadRequestError>().Select(x => new { x.Message, x.Metadata }));
            }

            return CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value);
        }


        /// <summary>
        /// Update an existing category
        /// </summary>
        /// <returns code="204">Category updated successfully</returns>
        /// <returns code="400">Unable to update the category due to validaton error</returns>
        /// <returns code="404">Category not found</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var result = await _categoryService.UpdateAsync(updateCategoryDto);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.OfType<IBadRequestError>().Select(x => new { x.Message, x.Metadata }));
            }

            if (result.HasError<INotFoundError>())
            {
                return NotFound(result.Errors.OfType<IBadRequestError>().Select(x => new { x.Message, x.Metadata }));
            }

            return NoContent();
        }

        /// <summary>
        /// Delete an existing category and replace all movements that has that category to a new category
        /// </summary>
        /// <returns code="204">Category deleted successfully</returns>
        /// <returns code="400">Unable to delete the category due to validaton error</returns>
        /// <returns code="404">Category not found</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id, [FromBody] ReplaceCategoryDto deleteCategoryDto)
        {
            var result = await _categoryService.DeleteAsync(id, deleteCategoryDto);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.OfType<IBadRequestError>().Select(x => new { x.Message, x.Metadata }));
            }

            if (result.HasError<INotFoundError>())
            {
                return NotFound(result.Errors.OfType<INotFoundError>().Select(x => new { x.Message, x.Metadata }));
            }

            return NoContent();
        }
    }
}
