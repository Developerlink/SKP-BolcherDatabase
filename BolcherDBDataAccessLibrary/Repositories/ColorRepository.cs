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
    public class ColorRepository : GenericRepository<Color, BolcherDBContext>, IColorRepository
    {
        public ColorRepository(BolcherDBContext context)
            : base(context)
        {
        }

        private async Task<ICollection<Color>> GetByFilterAsync(string filter)
        {
            return await Context.Colors.Where(c => EF.Functions.Like(c.Name, filter)).ToListAsync();
        }

        public async Task<ICollection<Color>> GetBySearchStartsWithAsync(string search_term)
        {
            string filter = $"{search_term.ToLower()}%";
            var colors = await GetByFilterAsync(filter);
            return colors;
        }

        Task<ICollection<Color>> ISearchable<Color>.GetByFilterAsync(string filter)
        {
            throw new NotImplementedException();
        }
    }
}