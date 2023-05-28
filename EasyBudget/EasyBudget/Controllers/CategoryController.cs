using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Errors.IErros;
using EasyBudget.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyBudget.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        /// <response code="200">Returns all categories in the system</response>
        [ProducesResponseType(typeof(IEnumerable<ReadCategoryDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _categoryService.GetAllAsync(userId);

            return Ok(result.Value);
        }

        /// <summary>
        /// Gets a category with the specified id
        /// </summary>
        /// <response code="200">Return the category with the specified id</response>
        /// <response code="400">The specified id is invalid</response>
        /// <response code="404">Category not found</response>
        [ProducesResponseType(typeof(ReadCategoryDto), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _categoryService.GetByIdAsync(id, userId);

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
        /// <response code="201">Category created successfully</response>
        /// <response code="400">Unable to create the category due to validaton error</response>
        [ProducesResponseType(typeof(ReadCategoryDto), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDto createCategoryDto)
        {
            createCategoryDto.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
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
        /// <response code="204">Category updated successfully</response>
        /// <response code="400">Unable to update the category due to validaton error</response>
        /// <response code="404">Category not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            updateCategoryDto.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _categoryService.UpdateAsync(updateCategoryDto);

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

        /// <summary>
        /// Delete an existing category and replace all movements that has that category to a new category
        /// </summary>
        /// <response code="204">Category deleted successfully</response>
        /// <response code="400">Unable to delete the category due to validaton error</response>
        /// <response code="404">Category not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] ReplaceCategoryDto deleteCategoryDto)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _categoryService.DeleteAsync(id, deleteCategoryDto, userId);

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
