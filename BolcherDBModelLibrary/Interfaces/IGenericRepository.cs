using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolcherDBModelLibrary.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(int id);
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task SaveAsync();
        Task UpdateAsync(T entity);
    }
}
