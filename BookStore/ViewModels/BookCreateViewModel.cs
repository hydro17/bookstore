using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
  public class BookCreateViewModel
  {
    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    public string Description { get; set; }

    [Range(0.01, 100000)]
    [Required]
    public decimal Price { get; set; }

    public IFormFile Photo { get; set; }
  }
}
