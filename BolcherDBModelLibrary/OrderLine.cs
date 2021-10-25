using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BolcherDBModelLibrary
{
    public partial class OrderLine
    {
        public int CandyId { get; set; }
        public int SalesOrderId { get; set; }
        [Required]
        public short Amount { get; set; }

        public virtual Candy Candy { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
    }
}
