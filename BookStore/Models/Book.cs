﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
  public class Book
  {
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    public string Description { get; set; }

    [Range(0.01, 100000)]
    public decimal? Price { get; set; }

    public string PhotoUniqueName { get; set; }
  }
}
