using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBModelLibrary.Interfaces
{
    public interface IOrderLineRepository : IGenericRepository<OrderLine>
    {
        Task AddOrderlinesAsync(ICollection<OrderLine> orderLines);
        Task DeleteOrderLinesBySalesOrderIdAsync(int id);
        Task<ICollection<OrderLine>> GetAllBySalesOrderIdAsync(int salesOrderId);

    }
}
