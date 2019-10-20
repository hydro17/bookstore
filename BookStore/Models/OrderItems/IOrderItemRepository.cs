using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.OrderItems
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task<OrderItem> GetByIdAsync(int id);
        Task<OrderItem> AddAsync(OrderItem orderItem);
        Task<OrderItem> UpdateAsync(OrderItem orderItemChandes);
        Task<OrderItem> DeleteAsync(int id);
    }
}
