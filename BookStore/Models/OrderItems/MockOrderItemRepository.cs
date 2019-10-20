using BookStore.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.OrderItems
{
    public class MockOrderItemRepository : IOrderItem
    {
        private IList<OrderItem> _orderItemList;

        public MockOrderItemRepository()
        {
            //_orderItemList = new List<OrderItem>
            //{
            //    new OrderItem { 
            //        Id = 1, 
            //        Book = new Book { Id  } 
            //    },
            //};
        }

        public OrderItem Add(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public OrderItem Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderItem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public OrderItem Update(OrderItem orderItemChandes)
        {
            throw new NotImplementedException();
        }
    }
}
