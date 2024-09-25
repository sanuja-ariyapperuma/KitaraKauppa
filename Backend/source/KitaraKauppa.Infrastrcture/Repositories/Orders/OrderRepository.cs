using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Shared;
using KitaraKauppa.Infrastrcture.Database;
using KitaraKauppa.Service.CustomExceptions;
using KitaraKauppa.Service.Repositories.Orders;
using KitaraKauppa.Service.Shared;
using Microsoft.EntityFrameworkCore;

namespace KitaraKauppa.Infrastrcture.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly KitaraKauppaDbContext _context;

        public OrderRepository(KitaraKauppaDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrder(Order order)
        {

            var savedOrder = _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return savedOrder.Entity;
        }

        public async Task<KKResult<IEnumerable<Order>>> GetAllOrders() {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Color)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Brand)
                .Include(o => o.User)
                .Include(o => o.Address)
                .ToListAsync();
            return new KKResult<IEnumerable<Order>>().SucceededWithValue(orders);
        }
            

        public async Task<Order?> GetOrder(Guid orderId)
        {
            return await _context.Orders
                                     .Include(o => o.OrderItems)
                                     .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<bool> UpdateOrder(Guid orderId, OrderStatus orderStatus)
        {
            var order = await _context.Orders
                                     .FindAsync(orderId);

            if (order == null)
            {
                throw new RecordNotFoundException("order");
            }

            order.OrderStatus = orderStatus;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}