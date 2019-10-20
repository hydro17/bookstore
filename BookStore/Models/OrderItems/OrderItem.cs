﻿using BookStore.Models.Books;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.OrderItems
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public Book Book { get; set; }
    }
}