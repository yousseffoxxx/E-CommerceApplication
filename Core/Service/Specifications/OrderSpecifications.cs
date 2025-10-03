namespace Service.Specifications
{
    internal class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(string email) 
            : base(order => order.UserEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);

            AddOrderBy(o => o.OrderDate);
        }

        public OrderSpecifications(Guid id) 
            : base(order => order.Id == id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
        }
    }
}
