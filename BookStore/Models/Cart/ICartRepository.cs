﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public interface ICartRepository
    {
        IEnumerable<OrderItem> GetAllCartItems();
        void AddToCart(int id);
        bool RemoveFromCart(int id);
    }
}