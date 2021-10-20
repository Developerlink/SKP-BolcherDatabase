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
    }
}
