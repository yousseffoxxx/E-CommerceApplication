namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException : NotFoundException
    {
        public DeliveryMethodNotFoundException(int id) : base($"No Delivery Method with Id = {id} was found")
        {
        }

        public DeliveryMethodNotFoundException(string message): base(message)
        {
            
        }
    }
}
