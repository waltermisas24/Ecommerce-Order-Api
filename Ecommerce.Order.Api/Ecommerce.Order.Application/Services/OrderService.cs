using Ecommerce.Order.Application.Interfaces;
using Ecommerce.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderInfo?> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task CreateOrderAsync(OrderInfo order)
        {
            order.Id = Guid.NewGuid().ToString();
            await _repository.AddAsync(order);
        }

        public async Task UpdateOrderItemsAsync(string orderId, List<OrderItem> items)
        {
            var order = await _repository.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            order.Items = items;

            await _repository.UpdateItemsAsync(orderId, items);
        }

    }
}
