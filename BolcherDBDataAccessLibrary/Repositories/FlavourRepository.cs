using BolcherDBModelLibrary;
using BolcherDBModelLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBDataAccessLibrary.Repositories
{
    public class FlavourRepository : GenericRepository<Flavour, BolcherDBContext>,
        IFlavourRepository
    {
        public FlavourRepository(BolcherDBContext context)
            : base(context)
        {
        }
    }
}
