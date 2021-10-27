using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BolcherDBModelLibrary
{
    public partial class OrderLineDto
    {
        public int CandyId { get; set; }
        public int SalesOrderId { get; set; }
        public short Amount { get; set; }

        public virtual CandyDto Candy { get; set; }
        public virtual SalesOrderDto SalesOrder { get; set; }
    }
}
