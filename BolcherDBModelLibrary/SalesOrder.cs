using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BolcherDBModelLibrary
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
