using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Domain.Entities
{
    public class OrderInfo
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public string Status { get; set; } = "Created";
    }
}
