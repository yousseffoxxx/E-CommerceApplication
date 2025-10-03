namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> LoginAsync(LoginDto loginModel);
        public Task<UserResultDto> RegisterAsync(RegisterDto loginModel);
        public Task<UserResultDto> GetUserByEmail(string email);
        public Task<bool> CheckEmailExist(string email);
        public Task<AddressDto> GetUserAddress(string email);
        public Task<AddressDto> UpdateUserAddress(AddressDto address, string email);
    }
}
