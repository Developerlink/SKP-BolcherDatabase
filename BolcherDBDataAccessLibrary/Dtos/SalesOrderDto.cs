using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BolcherDBModelLibrary
{
    public partial class SalesOrderDto
    {
        public SalesOrderDto(SalesOrder salesOrder)
        {
            Id = salesOrder.Id;
            CustomerId = salesOrder.CustomerId;
            OrderDate = salesOrder.OrderDate;

            Candies = new List<CandyDto>();
            foreach (var orderline in salesOrder.OrderLines)
            {
                Candies.Add(new CandyDto(orderline.Candy));
            }
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        ICollection<CandyDto> Candies { get; set; }
    }
}
