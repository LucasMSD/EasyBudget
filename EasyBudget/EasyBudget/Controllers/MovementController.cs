using EasyBudget.Data.Dto.MovementDto;
using EasyBudget.Errors;
using EasyBudget.Services.Implementations;
using EasyBudget.Services.IServices;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace EasyBudget.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovementController : ControllerBase
    {
        private readonly IMovementService _movementService;

        public MovementController(IMovementService movementService)
        {
            _movementService = movementService;
        }

        /// <summary>
        /// Gets all movements in the system
        /// </summary>
        /// <returns code="200">Returns all movements in the system</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _movementService.GetAllAsync();

            return Ok(result.Value);
        }

        /// <summary>
        /// Gets a movement with the specified id
        /// </summary>
        /// <returns code="200">Return the movement with the specified id</returns>
        /// <returns code="400">The specified id is invalid</returns>
        /// <returns code="404">Movement not found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var result = await _movementService.GetByIdAsync(id);

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
        /// Gets the total balance
        /// </summary>
        /// <returns code="200">Return the total balance</returns>
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
            => Ok((await _movementService.GetBalanceAsync()).Value);


        /// <summary>
        /// Create a new movement
        /// </summary>
        /// <returns code="201">Movement created successfully</returns>
        /// <returns code="400">Unable to create the movement due to validaton error</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMovementDto createMovementDto)
        {
            var result = await _movementService.CreateAsync(createMovementDto);

            if (result.HasError<IError>())
            {
                return BadRequest(result.Errors.Select(x => new { x.Message, x.Metadata }));
            }

            return CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value);
        }

        /// <summary>
        /// Update an existing movement
        /// </summary>
        /// <returns code="204">Movement updated successfully</returns>
        /// <returns code="400">Unable to update the movement due to validaton error</returns>
        /// <returns code="404">Movement or movement category not found</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateMovementDto updateMovementDto)
        {
            var result = await _movementService.UpdateAsync(updateMovementDto);

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
        /// Delete an existing movement
        /// </summary>
        /// <returns code="204">Movement deleted successfully</returns>
        /// <returns code="404">Movement not found</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var result = await _movementService.DeleteAsync(id);

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
