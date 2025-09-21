namespace Domain.Exceptions
{
    public sealed class OrderNotFoundException(Guid id) : NotFoundException($"No Order with Id = {id} was found")
    {
    }
}
