using BookStore.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.OrderItems
{
    public class MockOrderItemRepository : IOrderItemRepository
    {
        private IList<OrderItem> _orderItemList;

        public MockOrderItemRepository()
        {
            var _mockBookRepository = new MockBookRepository();

            _orderItemList = new List<OrderItem>
            {
                new OrderItem {
                    Id = 1,
                    Book = _mockBookRepository.GetById(1),
                    Quantity = 1
                },
                new OrderItem
                {
                    Id = 2,
                    Book = _mockBookRepository.GetById(2),
                    Quantity = 5
                },
                new OrderItem
                {
                    Id = 3,
                    Book = _mockBookRepository.GetById(3),
                    Quantity = 2
                }
            };
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        public async Task<OrderItem> AddAsync(OrderItem orderItem)
        {
            if (orderItem != null)
            {
                orderItem.Id = _orderItemList.Max(o => o.Id) + 1;
                _orderItemList.Add(orderItem);
            }

            return orderItem;
        }

        public async Task<OrderItem> DeleteAsync(int id)
        {
            OrderItem orderItem = _orderItemList.FirstOrDefault(o => o.Id == id);

            if (orderItem != null)
            {
                _orderItemList.Remove(orderItem);
            }

            return orderItem;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync() 
            => _orderItemList;

        public async Task<OrderItem> GetByIdAsync(int id) 
            => _orderItemList.FirstOrDefault(o => o.Id == id);

        public async Task<OrderItem> UpdateAsync(OrderItem orderItemChanges)
        {
            if (orderItemChanges == null) throw new ArgumentNullException(nameof(orderItemChanges));

            OrderItem orderItem = _orderItemList.FirstOrDefault(o => o.Id == orderItemChanges.Id);

            if (orderItem != null)
            {
                orderItem.Book = orderItemChanges.Book;
                orderItem.Quantity = orderItemChanges.Quantity;
            }

            return orderItem;
        }

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
