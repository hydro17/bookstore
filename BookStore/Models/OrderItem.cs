using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
  public class OrderItem
  {
    public int Id { get; set; }

    [Range(1,int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public Book Book { get; set; }
  }
}