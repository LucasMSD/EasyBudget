using EasyBudget.Data.Dto.MovementDto;
using EasyBudget.Errors;
using EasyBudget.Services.Implementations;
using EasyBudget.Services.IServices;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _movementService.GetAllAsync();

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var result = await _movementService.GetByIdAsync(id);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.Select(x => new { x.Message, x.Metadata }));
            }

            return Ok(result.Value);
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
            => Ok((await _movementService.GetBalanceAsync()).Value);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMovementDto createMovementDto)
        {
            var result = await _movementService.CreateAsync(createMovementDto);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.Select(x => new { x.Message, x.Metadata }));
            }

            return CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateMovementDto updateMovementDto)
        {
            var result = await _movementService.UpdateAsync(updateMovementDto);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.Select(x => new { x.Message, x.Metadata }));
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var result = await _movementService.DeleteAsync(id);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return NoContent();
        }
    }
}
