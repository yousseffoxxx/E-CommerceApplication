namespace Service.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Where

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        #endregion

        #region Include

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = []; // new List<Expression<Func<TEntity, object>>>();
        
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) 
            => IncludeExpressions.Add(includeExpression);
        #endregion

        #region Sorting

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
            => OrderBy = orderByExpression;

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
            => OrderBy = orderByDescendingExpression;

        #endregion

        #region Pagination
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPaginated { get; set; }

        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginated = true;
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
        }
        #endregion

        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteriaExpression)
        {
            Criteria = criteriaExpression;
        }
    }
}
