using BookStore.Models.OrderItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Order placed")]
        public DateTime OrderPlaced { get; set; }

        public DateTime? OrderFulfilled { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
