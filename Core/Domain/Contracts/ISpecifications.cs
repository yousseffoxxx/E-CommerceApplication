namespace Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        // Property Signature for Each Dynamic Part in the Query

        // Where()
        public Expression<Func<TEntity, bool>>? Criteria { get; }
        
        // Include()
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        //OrderBy()
        public Expression<Func<TEntity,object>> OrderBy { get; }
        
        //OrderByDescending()
        public Expression<Func<TEntity,object>> OrderByDescending { get; }

        // Pagination
        public int Skip { get; }
        public int Take { get; }
        public bool IsPaginated { get; set; }

    }
}
