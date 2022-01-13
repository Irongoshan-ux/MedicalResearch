namespace MedicineManaging.Domain.Interfaces
{
    public interface IRepository<TKey, TEntity>
    {
        Task<TEntity> FindByIdAsync(TKey entityId);
        Task AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<IEnumerable<TEntity>> FindByPageAsync(int page = 1, int pageSize = 5);
        Task DeleteAsync(TKey entityId);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TKey entityId, TEntity updatedEntity);
    }
}
