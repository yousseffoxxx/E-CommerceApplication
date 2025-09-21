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

        // GET BaseUrl/api/Authentication/EmailExist
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            var result = await _serviceManager.AuthenticationService.CheckEmailExist(email);

            return Ok(result);
        }

        // GET BaseUrl/api/Authentication
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.AuthenticationService.GetUserByEmail(email);
            
            return Ok(result);
        }

        // GET BaseUrl/api/Authentication/Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.AuthenticationService.GetUserAddress(email);
            
            return Ok(result);
        }

        // PUT BaseUrl/api/Authentication/Address
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.AuthenticationService.UpdateUserAddress(address, email);
            
            return Ok(result);
        }
    }
}
