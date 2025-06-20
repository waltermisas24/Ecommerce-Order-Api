using Ecommerce.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Domain.Interfaces;

public interface IOrderService
{
    Task CreateOrderAsync(OrderInfo order);
    Task UpdateOrderItemsAsync(string orderId, List<OrderItem> items);
    Task<OrderInfo?> GetByIdAsync(string id);
}
