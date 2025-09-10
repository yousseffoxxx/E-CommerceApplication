namespace Persistence
{
    static class SpecificationQueryBuilder
    {
        // Create Query
        // _dbContext.Set<TEntity>().where().Include().Include()

        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> entryPoint, ISpecifications<TEntity, TKey> specifications) 
            where TEntity : BaseEntity<TKey>
        {
            var query = entryPoint;

            if(specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);
            
            if(specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);
            

            if(specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);


            if(specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                // option 1
                //foreach (var expression in specifications.IncludeExpressions)
                //    query = query.Include(expression);

                // option 2
                query = specifications.IncludeExpressions
                    .Aggregate(query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
            }

            if (specifications.IsPaginated)
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            
            return query;
        }
    }
}
