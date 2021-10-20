using System;
using System.Collections.Generic;

#nullable disable

namespace BolcherDBModelLibrary
{
    public partial class Customer
    {
        public Customer()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
