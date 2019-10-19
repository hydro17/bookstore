using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Extensions;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        public IActionResult Index() => View(_cartRepository.GetAllOrderItems());

        public IActionResult AddToCart(int id)
        {
            _cartRepository.AddToCart(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartRepository.RemoveFromCart(id);
            return RedirectToAction(nameof(Index));
        }
    }
}