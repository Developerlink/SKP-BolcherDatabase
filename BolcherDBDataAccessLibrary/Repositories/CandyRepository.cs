using BolcherDBModelLibrary;
using BolcherDBModelLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBDataAccessLibrary.Repositories
{
    public class CandyRepository : GenericRepository<Candy, BolcherDBContext>,
        ISearchable<Candy>, ICandyRepository
    {
        public CandyRepository(BolcherDBContext context)
            : base(context)
        {
        }

        public async Task<ICollection<Candy>> GetCandiesByColorAsync(int colorId)
        {
            return await Context.Candies.Where(c => c.ColorId == colorId).ToListAsync();
        }

        public async Task<ICollection<Candy>> GetCandiesByTwoColorsAsync(int colorOneId, int colorTwoId)
        {
            return await Context.Candies.Where(c => c.ColorId == colorOneId || c.ColorId == colorTwoId).ToListAsync();
        }

        public async Task<ICollection<Candy>> GetCandiesNotOfColor(int colorId)
        {
            return await Context.Candies.Where(c => c.ColorId != colorId).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<ICollection<Candy>> GetByFilterAsync(string filter)
        {
            return await Context.Candies.Where(c => EF.Functions.Like(c.Name.ToLower(), filter)).ToListAsync();
        }

        public async Task<ICollection<Candy>> GetBySearchStartsWithAsync(string searchTerm)
        {
            string filter = $"{searchTerm.ToLower()}%";
            var candies = await GetByFilterAsync(filter);
            return candies;
        }

        public async Task<ICollection<Candy>> GetBySearchContainsAsync(string searchTerm)
        {
            string filter = $"%{searchTerm.ToLower()}%";
            var candies = await GetByFilterAsync(filter);
            return candies;
        }

        public async Task<ICollection<Candy>> GetByWeightLowerThan(int weight)
        {
            return await Context.Candies.Where(c => c.Weight < weight).OrderBy(c => c.Weight).ToListAsync();
        }

        public async Task<ICollection<Candy>> GetByWeightBetween(int lowerWeightLimit, int upperWeightLimit)
        {
            return await Context.Candies.Where(c => c.Weight >= lowerWeightLimit && c.Weight <= upperWeightLimit).OrderBy(c => c.Name).ToListAsync();
        }

        public bool HasUniqueName(int id, string name)
        {
            var entity = Context.Strengths.FirstOrDefault(s => s.Name.Trim().ToUpper() == name.Trim().ToUpper() && s.Id != id);
            if (entity == null)
                return true;
            return false;
        }
    }
}
