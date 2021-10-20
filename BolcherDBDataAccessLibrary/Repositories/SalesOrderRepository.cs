using BolcherDBModelLibrary;
using BolcherDBModelLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBDataAccessLibrary.Repositories
{
    public class SalesOrderRepository : GenericRepository<SalesOrder, BolcherDBContext>,
        ISalesOrderRepository
    {
        public SalesOrderRepository(BolcherDBContext context)
            : base(context)
        {
        }
    }
}
