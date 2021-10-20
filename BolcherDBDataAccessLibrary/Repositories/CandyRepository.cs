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

        public async Task<ICollection<Candy>> GetByFilterAsync(string filter)
        {
            return await Context.Candies.Where(c => EF.Functions.Like(c.Name, filter)).ToListAsync();
        }

        public async Task<ICollection<Candy>> GetBySearchStartsWithAsync(string searchTerm)
        {
            string filter = $"{searchTerm.ToLower()}%";
            var candies = await GetByFilterAsync(filter);
            return candies;
        }
    }
}
