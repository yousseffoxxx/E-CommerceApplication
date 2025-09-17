using System.Globalization;

namespace Shared.DTOs.Authentication
{
    public record RegisterDto
    {
        [Required(ErrorMessage ="Display Name Is Required")]
        public string DisplayName { get; init; }
        
        [Required(ErrorMessage ="User Name Is Required")]
        public string UserName { get; init; }
        
        [Phone]
        public string? PhoneNumber { get; init; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
