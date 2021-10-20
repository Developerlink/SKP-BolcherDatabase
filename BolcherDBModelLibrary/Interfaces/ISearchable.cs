using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolcherDBModelLibrary.Interfaces
{
    public interface ISearchable<T>
    {
        Task<ICollection<T>> GetByFilterAsync(string filter);
        Task<ICollection<T>> GetBySearchStartsWithAsync(string searchTerm);
    }
}
