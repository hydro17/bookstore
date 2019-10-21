using BookStore.Models.Books;
using BookStore.Models.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.OrderItems
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        //public int BookId { get; set; }

        //[ForeignKey("BookId")]
        public Book Book { get; set; }

        //public int OrderId { get; set; }

        //[ForeignKey("OrderId")]
        //public Order Order { get; set; }
    }
}