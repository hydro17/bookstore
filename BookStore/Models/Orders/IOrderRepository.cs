using BookStore.Models.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Orders
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetAllByCustomerIdAsync(int customerId);
        Task<Order> GetByIdAsync(int orderId);
        Task<Order> AddOrderMadeFromCartContentAsync();
        Task<Order> UpdateAsync(Order orderChanges);
        Task<Order> DeleteAsync(int orderId);

        //void AddOrderItemToOrder(OrderItem orderItem);
        bool RemoveOrderItemFromOrder(OrderItem orderItem);
    }
}
