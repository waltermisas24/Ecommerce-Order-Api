using Ecommerce.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Domain.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(OrderInfo order);
    Task<OrderInfo?> GetByIdAsync(string id);
    Task UpdateItemsAsync(string orderId, List<OrderItem> items);
}
