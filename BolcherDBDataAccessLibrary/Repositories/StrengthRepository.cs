using BolcherDBDataAccessLibrary;
using BolcherDBModelLibrary.Interfaces;
using BolcherDBModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBDataAccessLibrary.Repositories
{
    public class StrengthRepository : GenericRepository<Strength, BolcherDBContext>,
        IStrengthRepository
    {
        public StrengthRepository(BolcherDBContext context)
            : base(context)
        {
        }
    }
}
