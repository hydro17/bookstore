using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Extensions;
using BookStore.Models.Books;
using BookStore.Models.Carts;
using BookStore.Models.OrderItems;
using BookStore.Utilities;
using Microsoft.AspNetCore.Http;

namespace BookStore.Models.Orders
{
    public class MockOrderRepository : IOrderRepository
    {
        IList<Order> _ordersList;

        public MockOrderRepository()
        {
            this._ordersList = new List<Order>();
        }

        public async Task<Order> AddAsync(Order order)
        {
            order.Id = (_ordersList.Count > 0) ? _ordersList.Max(o => o.Id) + 1 : 1;
            _ordersList.Add(order);
            return order;
        }

        public async Task<Order> DeleteAsync(int orderId)
        {
            Order order = _ordersList.FirstOrDefault(o => o.Id == orderId);

            if (order != null)
            {
                _ordersList.Remove(order);
            }

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync() 
            => _ordersList;

        public async Task<IEnumerable<Order>> GetAllByCustomerIdAsync(int customerId)
        {
            //_ordersList.Where(o => o.CustomerId == customerId)
            throw new NotImplementedException();
        }

        public async Task<Order> GetByIdAsync(int orderId) 
            => _ordersList.FirstOrDefault(o => o.Id == orderId);

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
