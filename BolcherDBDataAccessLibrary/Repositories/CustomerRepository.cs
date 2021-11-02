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
    public class CustomerRepository : GenericRepository<Customer, BolcherDBContext>,
        ICustomerRepository
    {
        public CustomerRepository(BolcherDBContext context)
            : base(context)
        {
        }

        public async Task<ICollection<Customer>> GetByFilterAsync(string filter)
        {
            return await Context.Customers.Where(c => EF.Functions.Like(c.FirstName, filter)).ToListAsync();
        }

        public async Task<ICollection<Customer>> GetBySearchStartsWithAsync(string searchTerm)
        {
            var filter = $"{searchTerm.ToLower()}%";
            var customers = await GetByFilterAsync(filter);
            return customers;
        }

        public async Task<ICollection<Customer>> GetCustomersWhoBoughtSpecificCandy(int candyId)
        {
            var customers = await Context.OrderLines.Where(o => o.CandyId == candyId)
                .Select(o => o.SalesOrder)
                .Select(s => s.Customer)
                .Include(c => c.SalesOrders)
                .ThenInclude(s => s.OrderLines)
                .ThenInclude(o => o.Candy)
                .ToListAsync();
            return customers;
        }

        public async Task<ICollection<Customer>> GetCustomersWithSalesOrders()
        {
            var customers = await Context.OrderLines
                .Select(o => o.SalesOrder)
                .Select(s => s.Customer)
                .Distinct()
                .Include(c => c.SalesOrders)
                .ThenInclude(s => s.OrderLines)
                .ThenInclude(o => o.Candy)
                .ToListAsync();
            return customers;
        }
    }
}
