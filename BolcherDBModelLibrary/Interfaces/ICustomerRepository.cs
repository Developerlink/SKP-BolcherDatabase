using BolcherDBModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBModelLibrary.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>, ISearchable<Customer>
    {
        Task<ICollection<Customer>> GetCustomersWithSalesOrders();
        Task<ICollection<Customer>> GetCustomersWhoBoughtSpecificCandy(int candyId);
        Task<ICollection<Customer>> GetCustomersWhoBoughtCandyWithStrength(int candyStrengthId);
    }
}
