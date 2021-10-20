using System;
using System.Collections.Generic;

#nullable disable

namespace BolcherDBModelLibrary
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
