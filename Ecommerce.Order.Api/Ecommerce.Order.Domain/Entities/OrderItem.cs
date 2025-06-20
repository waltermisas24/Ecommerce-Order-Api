using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Order.Domain.Entities
{
    public class OrderItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public string? OrderInfoId { get; set; }
        [JsonIgnore]
        public OrderInfo? OrderInfo { get; set; }
    }

}
