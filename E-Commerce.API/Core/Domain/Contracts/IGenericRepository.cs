using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        // GetAll
        public Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);
        // GetById
        public Task<TEntity?> GetByIdAsync(TKey id);
        // Add
        public Task AddAsync(TEntity entity);
        // Update
        public void Update(TEntity entity);
        // Delete
        public void Delete(TEntity entity);

        #region Specifications
        public Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
        public Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
        #endregion
    }
}
