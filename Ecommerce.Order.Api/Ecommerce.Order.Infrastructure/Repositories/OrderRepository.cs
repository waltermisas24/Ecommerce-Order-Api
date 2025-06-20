using Ecommerce.Order.Domain.Entities;
using Ecommerce.Order.Infrastructure.Data;
using Ecommerce.Order.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderInfo order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderInfo?> GetByIdAsync(string id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateItemsAsync(string orderId, List<OrderItem> items)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new Exception("Order not found");

            var existingItems = await _context.OrderItem
                .Where(i => i.OrderInfoId == orderId)
                .ToListAsync();

            _context.OrderItem.RemoveRange(existingItems);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                item.Id = Guid.NewGuid().ToString();
                item.OrderInfoId = orderId;
                item.OrderInfo = null;
            }

            await _context.OrderItem.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

    }
}
