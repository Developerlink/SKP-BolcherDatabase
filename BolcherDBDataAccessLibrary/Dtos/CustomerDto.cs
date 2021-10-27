using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BolcherDBModelLibrary
{
    public partial class CustomerDto
    {
        public CustomerDto(Customer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;

            SalesOrders = new List<SalesOrderDto>();
            foreach (var salesorder in customer.SalesOrders)
            {
                SalesOrders.Add(new SalesOrderDto(salesorder));
            }
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        ICollection<SalesOrderDto> SalesOrders { get; set; }
    }
}
