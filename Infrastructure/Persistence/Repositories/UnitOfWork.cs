namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        // Dictionary<string, object> => string: key [Name of type] , object: value [object form Generic Repository]
        private readonly Dictionary<string, object> _repositories = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // Get Type Name
            var typeName = typeof(TEntity).Name;

            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;

            else
            {
                // Create object
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);
                // store object in Dictionary
                _repositories[typeName] = repo;
                // return object
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
