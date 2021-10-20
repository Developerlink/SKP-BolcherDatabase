using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolcherDBModelLibrary.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(T entity);
        Task<bool> ExistsAsync(int id);
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> SaveAsync();
        Task<bool> UpdateAsync(T entity);
    }
}
