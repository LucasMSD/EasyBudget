using EasyBudget.Data.Dto.MovementDto;
using EasyBudget.Data.Dto.UserDto;
using EasyBudget.Errors.IErros;
using EasyBudget.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBudget.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Sign up a new user
        /// </summary>
        /// <param name="userSignupDto"></param>
        /// <response code="201">The user was signed up successfully</response>
        /// <response code="400">Unable to sign up the user due to validaton error</response>
        [HttpPost("Signup")]
        [ProducesResponseType(typeof(ReadUserSignupDto), StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] UserSignupDto userSignupDto)
        {
            var result = await _userService.Signup(userSignupDto);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.OfType<IBadRequestError>().Select(x => new { x.Message, x.Metadata}));
            }

            return StatusCode(201, result.Value);
        }

        /// <summary>
        /// Sign in a user
        /// </summary>
        /// <param name="userSiginDto"></param>
        /// <response code="400">Unable to sign in the user due to validaton error</response>
        [HttpPost("Signin")]
        [ProducesResponseType(typeof(TokenLoginDto), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> Signin([FromBody] UserSigninDto userSiginDto)
        {
            var result = await _userService.Signin(userSiginDto);

            if (result.HasError<IBadRequestError>())
            {
                return BadRequest(result.Errors.OfType<IBadRequestError>().Select(x => new { x.Message, x.Metadata}));
            }

            return Ok(result.Value);
        }
    }
}
