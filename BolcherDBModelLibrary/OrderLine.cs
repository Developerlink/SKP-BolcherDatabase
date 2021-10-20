using System;
using System.Collections.Generic;

#nullable disable

namespace BolcherDBModelLibrary
{
    public partial class OrderLine
    {
        public int CandyId { get; set; }
        public int SalesOrderId { get; set; }
        public short Amount { get; set; }

        public virtual Candy Candy { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
    }
}
