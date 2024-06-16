using AuthenticationApi.Infrastructure;
using AuthenticationClassLibrary;
using AuthenticationClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSettings _settings;
        private readonly IUserServiceAsync _userService;

        public UserController(IOptions<AppSettings> options, IUserServiceAsync service)
        {
            _settings = options.Value;
            _userService = service;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(model.Username == null || model.Password == null)
            {
                return BadRequest("Bad Username/Password. Please Check your Credentials and Try Again.");
            }
            var user = await _userService.AuthenticateAsync(model);
            if (user is null)
            {
                return BadRequest("Bad Username/Password. Please Check your Credentials and Try Again.");
            }
            var token = TokenManager.GenerateWebToken(user, _settings);
            var authResponse = new AuthenticationResponse(user, token);
            return authResponse;
        }

        // URL: api/accounts/validate
        [HttpGet(template: "validate")]
        public async Task<ActionResult<User>> Validate()
        {
            var user = HttpContext.Items["User"] as User;
            if (user is null)
            {
                return Unauthorized("You are not authorized to access this application.");
            }
            return user;
        }
    }
}
