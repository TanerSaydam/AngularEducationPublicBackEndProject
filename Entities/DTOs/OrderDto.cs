using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OrderDto : IDto
    {
        public Payment Payment { get; set; }
        public List<Order> Orders { get; set; }
        public decimal Total { get; set; }
    }
}
