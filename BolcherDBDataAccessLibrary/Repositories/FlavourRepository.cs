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

        public bool HasUniqueName(int id, string name)
        {
            var entity = Context.Strengths.FirstOrDefault(s => s.Name.Trim().ToUpper() == name.Trim().ToUpper() && s.Id != id);
            if (entity == null)
                return true;
            return false;
        }
    }
}
