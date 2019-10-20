using BookStore.Models.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Orders
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetAllByCustomerId(int userId);
        Order GetById(int orderId);
        Order Add(Order order);
        Order Update(Order orderChanges);
        Order Delete(int orderId);
        
        void AddOrderItemToOrder(OrderItem orderItem);
        bool RemoveOrderItemFromOrder(OrderItem orderItem);
    }
}
