using EasyBudget.Data.Dto.MovementDto;
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

            if (result.IsFailed)
            {
                return NoContent();
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var result = await _movementService.GetByIdAsync(id);
            
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMovementDto createMovementDto)
        {
            var result = await _movementService.CreateAsync(createMovementDto);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateMovementDto updateMovementDto)
        {
            var result = await _movementService.UpdateAsync(updateMovementDto);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok();
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
