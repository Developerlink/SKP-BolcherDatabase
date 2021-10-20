using BolcherDBModelLibrary;
using BolcherDBModelLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBDataAccessLibrary.Repositories
{
    public class SournessRepository : GenericRepository<Sourness, BolcherDBContext>,
        ISournessRepository
    {
        public SournessRepository(BolcherDBContext context)
            : base(context)
        {
        }
    }
}
