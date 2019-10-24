using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.OrderItems;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.Orders
{
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public SqlOrderRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<Order> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> DeleteAsync(int orderId)
        {
            Order order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);

            if (order != null)
            {
                // Delete all items belonging to the order before deleting it
                // TODO: move removing OrderItems to SqlOrderItemRepository
                _context.OrderItems.RemoveRange(order.OrderItems);

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
            => await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book)
                .ToListAsync();

        public async Task<IEnumerable<Order>> GetAllSortedByOrderPlacedDescAsync()
            => await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book)
                .OrderByDescending(o => o.OrderPlaced)
                .ToListAsync();

        public async Task<IEnumerable<Order>> GetAllByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetByIdAsync(int orderId)
            => await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book)
                .FirstOrDefaultAsync(o => o.Id == orderId);

        public bool RemoveOrderItemFromOrder(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> UpdateAsync(Order orderChanges)
        {
            throw new NotImplementedException();
        }
    }
}
