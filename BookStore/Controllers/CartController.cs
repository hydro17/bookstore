using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Extensions;
using BookStore.Models;
using BookStore.Models.Books;
using BookStore.Models.Carts;
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

        public IActionResult Index() 
            => View(_cartRepository.GetAllCartItemsSortedAscBy(cartItem => cartItem.Book.Title));

        public IActionResult AddToCart(int id, bool? backToShopping)
        {
            _cartRepository.AddToCart(id);

            if (backToShopping != null && backToShopping == true)
            {
                return RedirectToAction(actionName: nameof(Index), controllerName: nameof(Book));
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartRepository.RemoveFromCart(id);
            return RedirectToAction(nameof(Index));
        }
    }
}