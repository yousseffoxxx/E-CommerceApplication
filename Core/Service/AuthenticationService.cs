namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IOptions<JwtOptions> _options) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginModel)
        {
            // check if there is a user under this email
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user is null) throw new UnAuthorizedException("Email Doesn't Exist");

            // check if the password is correct for this email
            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if (!result) throw new UnAuthorizedException();

            // Create Token [JWT] and return response

            return new UserResultDto(user.DisplayName, user.Email, await CreateTokenAsync(user));
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerModel)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerModel.DisplayName,
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }

            return new UserResultDto(user.DisplayName, user.Email, await CreateTokenAsync(user));
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var jwtOptions = _options.Value;
            
            // payload/Private Claims [user defined]
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            // add Roles to claims if exist
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            // secret Key
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));

            // Signing Credentials [secretKey, signing algorithm]
            var signingCreds = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256);

            // Token
            var token = new JwtSecurityToken(
                audience: jwtOptions.Audience,
                issuer: jwtOptions.Issuer,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                claims: authClaims,
                signingCredentials: signingCreds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
