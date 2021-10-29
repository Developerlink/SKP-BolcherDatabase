using BolcherDBModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBModelLibrary.Interfaces
{
    public interface ICandyRepository : IGenericRepository<Candy>, ISearchable<Candy>, IUniqueNameable
    {
        Task<ICollection<Candy>> GetBySearchContainsAsync(string searchTerm);
        Task<ICollection<Candy>> GetBySearchStartsWithAndColorIdAsync(string searchTerm, int colorId);
        Task<ICollection<Candy>> GetBySearchContainsAndColorAsync(string searchTerm, int colorId);
        Task<ICollection<Candy>> GetByWeightBetween(int lowerWeightLimit, int upperWeightLimit);
        Task<ICollection<Candy>> GetByWeightLowerThan(int weight);
        Task<ICollection<Candy>> GetCandiesByColorAsync(int colorId);
        Task<ICollection<Candy>> GetCandiesByTwoColorsAsync(int colorOneId, int colorTwoId);
        Task<ICollection<Candy>> GetCandiesNotOfColor(int colorId);
        Candy GetRandomCandy();
        Task<ICollection<Candy>> GetHeaviestBy(int amount);
    }
}
