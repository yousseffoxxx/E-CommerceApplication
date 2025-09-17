namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> LoginAsync(LoginDto loginModel);
        public Task<UserResultDto> RegisterAsync(RegisterDto loginModel);
    }
}
