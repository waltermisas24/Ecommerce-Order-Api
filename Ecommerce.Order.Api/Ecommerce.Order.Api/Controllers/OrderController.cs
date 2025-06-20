using Ecommerce.Order.Domain.Interfaces;
using Ecommerce.Order.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Order.Api.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST /api/v1/orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderInfo order)
        {
            try
            {
                await _orderService.CreateOrderAsync(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el pedido: {ex.Message}");
            }
        }

        // PUT /api/v1/orders/{id}/items
        [HttpPut("{id}/items")]
        public async Task<IActionResult> UpdateItems(string id, [FromBody] List<OrderItem> items)
        {
            try
            {
                await _orderService.UpdateOrderItemsAsync(id, items);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar los items: {ex.Message} - {ex.InnerException?.Message}");
            }
        }

        // GET /api/v1/orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

    }
}
