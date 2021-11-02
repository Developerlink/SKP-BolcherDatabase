using BolcherDBModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBModelLibrary.Interfaces
{
    public interface ISalesOrderRepository : IGenericRepository<SalesOrder>
    {
        Task<ICollection<SalesOrder>> GetAllSortedByDate();
        Task<SalesOrder> GetByLatestOrderDate();
    }
}
