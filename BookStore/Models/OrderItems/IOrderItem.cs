using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.OrderItems
{
    public interface IOrderItem
    {
        IEnumerable<OrderItem> GetAll();
        OrderItem GetById(int id);
        OrderItem Add(OrderItem orderItem);
        OrderItem Update(OrderItem orderItemChandes);
        OrderItem Delete(int id);
    }
}
