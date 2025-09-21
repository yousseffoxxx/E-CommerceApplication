namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int id) 
        : NotFoundException($"No Delivery Method with Id = {id} was found")
    {
    }
}
