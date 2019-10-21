using BookStore.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.OrderItems
{
    public class SqlOrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;

        public SqlOrderItemRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<OrderItem> AddAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }

        public async Task<OrderItem> DeleteAsync(int id)
        {
            OrderItem orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }

            return orderItem;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
            => await _context.OrderItems.Include(orderItem => orderItem.Book).ToListAsync();

        public async Task<OrderItem> GetByIdAsync(int id)
            => await _context.OrderItems
            .Include(orderItem => orderItem.Book)
            .FirstOrDefaultAsync(orderItem => orderItem.Id == id);

        public async Task<OrderItem> UpdateAsync(OrderItem orderItemChanges)
        {
            EntityEntry orderItemEntityEntry = _context.OrderItems.Attach(orderItemChanges);
            orderItemEntityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();

            return orderItemChanges; 
        }
    }
}
