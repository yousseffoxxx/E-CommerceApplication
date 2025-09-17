namespace Presentation.Controllers
{
    // BaseUrl/api/Authentication
    public class AuthenticationController(IServiceManager _serviceManager) : ApiController
    {
        // POST BaseUrl/api/Authentication/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        {
            var result = await _serviceManager.AuthenticationService.LoginAsync(loginDto);

            return Ok(result);
        }

        // POST BaseUrl/api/Authentication/Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
        {
            var result = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);

            return Ok(result);
        }
    }
}
