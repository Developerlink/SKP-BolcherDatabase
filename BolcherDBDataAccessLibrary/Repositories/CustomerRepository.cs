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

        public async override Task<Customer> GetByIdAsync(int id)
        {
            return await Context.Customers.Where(c => c.Id == id)
                .Include(c => c.SalesOrders)
                .ThenInclude(s => s.OrderLines)
                .ThenInclude(o => o.Candy)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Customer>> GetBySearchStartsWithAsync(string searchTerm)
        {
            var filter = $"{searchTerm.ToLower()}%";
            var customers = await GetByFilterAsync(filter);
            return customers;
        }

        public async Task<ICollection<Customer>> GetCustomersWhoBoughtSpecificCandy(int candyId)
        {
            List<int> customerIds = await Context.Customers
                .FromSqlInterpolated($"SELECT Customer.id FROM Customer INNER JOIN SalesOrder ON SalesOrder.CustomerId = Customer.id INNER JOIN OrderLine ON OrderLine.SalesOrderId = SalesOrder.Id INNER JOIN Candy ON Candy.Id = OrderLine.CandyId WHERE CandyId = {candyId}")
                .Select(c => c.Id)
                .ToListAsync();

            var customers = await Context.Customers.Where(c => customerIds.Contains(c.Id))
                .Include(c => c.SalesOrders)
                .ThenInclude(s => s.OrderLines)
                .ThenInclude(o => o.Candy)
                .AsSplitQuery()
                .ToListAsync();
            return customers;
        }

        public async Task<ICollection<Customer>> GetCustomersWhoBoughtCandyWithStrength(int candyStrengthId)
        {
            List<int> customerIds = await Context.Customers
                .FromSqlInterpolated($"SELECT Customer.id FROM Customer INNER JOIN SalesOrder ON SalesOrder.CustomerId = Customer.id INNER JOIN OrderLine ON OrderLine.SalesOrderId = SalesOrder.Id INNER JOIN Candy ON Candy.Id = OrderLine.CandyId INNER JOIN Strength ON Strength.Id = Candy.StrengthId WHERE StrengthId = {candyStrengthId}")
                .Select(c => c.Id)
                .ToListAsync();

            var customers = await Context.Customers.Where(c => customerIds.Contains(c.Id))
                .Include(c => c.SalesOrders)
                .ThenInclude(s => s.OrderLines)
                .ThenInclude(o => o.Candy)
                .AsSplitQuery()
                .ToListAsync();
            return customers;
        }

        public async Task<ICollection<Customer>> GetCustomersWithSalesOrders()
        {
            var customers = await Context.Customers.Where(c => c.SalesOrders.Count > 0)
                .Include(c => c.SalesOrders)
                .ThenInclude(s => s.OrderLines)
                .ThenInclude(o => o.Candy)
                .ToListAsync();
            return customers;
        }

        public async override Task<ICollection<Customer>> GetAllAsync()
        {
            return await Context.Customers
                .Include(c => c.SalesOrders)
                .OrderByDescending(c => c.SalesOrders.Count)
                .ToListAsync();
        }
    }
}
