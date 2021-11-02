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
    public class SalesOrderRepository : GenericRepository<SalesOrder, BolcherDBContext>,
        ISalesOrderRepository
    {
        public SalesOrderRepository(BolcherDBContext context)
            : base(context)
        {
        }

        public override async Task<ICollection<SalesOrder>> GetAllAsync()
        {
            return await Context.SalesOrders
                .Include(x => x.OrderLines)
                .Include(x => x.Customer)
                .ToListAsync();
        }

        public async Task<ICollection<SalesOrder>> GetAllSortedByDate()
        {
            return await Context.SalesOrders.OrderBy(x => x.OrderDate)
                .Include(x => x.Customer)
                .Include(x => x.OrderLines)
                .ToListAsync();
        }

        public override async Task<SalesOrder> GetByIdAsync(int id)
        {
            return await Context.SalesOrders.Where(x => x.Id == id)
                .Include(x => x.OrderLines)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync();
        }

        public async Task<SalesOrder> GetByLatestOrderDate()
        {
            return await Context.SalesOrders.OrderByDescending(x => x.OrderDate)
                .Include(x => x.Customer)
                .Include(x => x.OrderLines)
                .ThenInclude(o => o.Candy)
                .FirstOrDefaultAsync();
        }
    }
}
