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
    public class MockOrderRepositoy : IOrderRepository
    {
        IList<Order> _ordersList;

        private readonly ICartRepository _cartRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public MockOrderRepositoy(
            ICartRepository cartRepository,
            IBookRepository bookRepository,
            IOrderItemRepository orderItemRepository)
        {
            this._ordersList = new List<Order>();

            this._cartRepository = cartRepository;
            this._bookRepository = bookRepository;
            this._orderItemRepository = orderItemRepository;
        }

        private async Task<IEnumerable<OrderItem>> GetAllOrderItemsFromCartContent()
        {
            IEnumerable<OrderItem> orderItems = _cartRepository.GetProductIdList()
                .GroupBy(
                bookId => bookId,
                bookId => bookId,
                (key_bookId, value_bookIdCollection) => new OrderItem
                {
                    Book = _bookRepository.GetById(key_bookId),
                    Quantity = value_bookIdCollection.Count()
                });

            return orderItems;
        }

        private async Task<IList<OrderItem>> GetOrderItemsWithAssignedId()
        {
            IList<OrderItem> orderItemsWithId = new List<OrderItem>();

            foreach (OrderItem orderItem in await this.GetAllOrderItemsFromCartContent())
            {
                orderItemsWithId.Add(await _orderItemRepository.AddAsync(orderItem));
            }

            return orderItemsWithId;
        }

        public async Task<Order> AddOrderMadeFromCartContentAsync()
        {
            Order order = new Order
            {
                OrderPlaced = DateTime.UtcNow,
                OrderItems = await this.GetOrderItemsWithAssignedId()
            };

            _ordersList.Add(order);

            return order;
        }

        public async Task<Order> DeleteAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllAsync() 
            => _ordersList;

        public async Task<IEnumerable<Order>> GetAllByCustomerIdAsync(int customerId)
        {
            //_ordersList.Where(o => o.CustomerId == customerId)
            throw new NotImplementedException();
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

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
