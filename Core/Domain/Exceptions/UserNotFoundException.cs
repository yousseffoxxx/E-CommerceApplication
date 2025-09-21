namespace Domain.Exceptions
{
    public class UserNotFoundException(string email) : NotFoundException($"User with email = {email} is not Found")
    {
    }
}
