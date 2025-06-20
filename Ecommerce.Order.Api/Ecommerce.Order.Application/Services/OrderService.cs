using Ecommerce.Order.Domain.Interfaces;
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
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderInfo?> GetByIdAsync(string id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task CreateOrderAsync(OrderInfo order)
        {
            if (string.IsNullOrEmpty(order.Id))
                order.Id = Guid.NewGuid().ToString();

            foreach (var item in order.Items)
            {
                if (string.IsNullOrEmpty(item.Id))
                    item.Id = Guid.NewGuid().ToString();

                item.OrderInfoId = order.Id;
            }

            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderItemsAsync(string orderId, List<OrderItem> items)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            order.Items = items;

            await _orderRepository.UpdateItemsAsync(orderId, items);
        }

    }
}
