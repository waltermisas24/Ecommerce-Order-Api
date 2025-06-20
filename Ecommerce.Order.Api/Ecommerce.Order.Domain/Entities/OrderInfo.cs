using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Domain.Entities
{
    public class OrderInfo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CustomerId { get; set; } = default!;
        public string Status { get; set; } = "Created";
        public List<OrderItem> Items { get; set; } = new();
    }
}
