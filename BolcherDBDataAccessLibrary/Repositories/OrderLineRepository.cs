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
    public class OrderLineRepository : GenericRepository<OrderLine, BolcherDBContext>, IOrderLineRepository
    {
        public OrderLineRepository(BolcherDBContext context)
            : base(context)
        {
        }

        public async Task AddOrderlinesAsync(ICollection<OrderLine> orderLines)
        {
            Context.AddRange(orderLines);
            await SaveAsync();
        }

        public async Task DeleteOrderLinesBySalesOrderIdAsync(int id)
        {
            var orderLines = await GetAllBySalesOrderIdAsync(id);
            Context.RemoveRange(orderLines);
            await SaveAsync();
        }

        public async Task<ICollection<OrderLine>> GetAllBySalesOrderIdAsync(int salesOrderId)
        {
            var orderLines = await Context.OrderLines.Where(x => x.SalesOrderId == salesOrderId).ToListAsync();
            return orderLines;
        }
    }
}
