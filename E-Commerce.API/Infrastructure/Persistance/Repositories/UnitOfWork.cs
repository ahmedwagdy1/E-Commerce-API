namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
            => (IGenericRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, TKey>(_dbContext));
            //return new GenericRepository<TEntity, TKey>(_dbContext);
            //var key = typeof(TEntity).Name;
            //if(!_repositories.ContainsKey(key))
            //    _repositories[key] = new GenericRepository<TEntity, TKey>(_dbContext);
            //return (IGenericRepository<TEntity, TKey>) _repositories[key];

        public async Task<int> SaveChangesAsync() 
            => await _dbContext.SaveChangesAsync();
    }
}
