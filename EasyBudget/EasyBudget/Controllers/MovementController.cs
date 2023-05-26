using EasyBudget.Data.Dto.MovementDto;
using EasyBudget.Errors;
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
        /// <response code="200">Returns all movements in the system</response>
        [ProducesResponseType(typeof(IEnumerable<ReadMovementDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _movementService.GetAllAsync();

            return Ok(result.Value);
        }

        /// <summary>
        /// Gets a movement with the specified id
        /// </summary>
        /// <response code="200">Return the movement with the specified id</response>
        /// <response code="400">The specified id is invalid</response>
        /// <response code="404">Movement not found</response>
        [ProducesResponseType(typeof(ReadMovementDto), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadMovementDto>> Get([FromRoute] int id)
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
        /// <response code="200">Return the total balance</response>
        [ProducesResponseType(typeof(ReadBalanceDto), StatusCodes.Status200OK)]
        [HttpGet("balance")]
        public async Task<ActionResult<ReadBalanceDto>> GetBalance()
            => Ok((await _movementService.GetBalanceAsync()).Value);


        /// <summary>
        /// Create a new movement
        /// </summary>
        /// <response code="201">Movement created successfully</response>
        /// <response code="400">Unable to create the movement due to validaton error</response>
        [ProducesResponseType(typeof(ReadMovementDto), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<ReadMovementDto>> Post([FromBody] CreateMovementDto createMovementDto)
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
        /// <response code="204">Movement updated successfully</response>
        /// <response code="400">Unable to update the movement due to validaton error</response>
        /// <response code="404">Movement or movement category not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        /// <response code="204">Movement deleted successfully</response>
        /// <response code="404">Movement not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
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
