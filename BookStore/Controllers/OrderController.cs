using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Models.Orders;
using BookStore.Models.Carts;
using BookStore.Models.OrderItems;
using BookStore.Models.Books;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderController(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IBookRepository bookRepository,
            IOrderItemRepository orderItemRepository)
        {
            this._orderRepository = orderRepository;
            this._cartRepository = cartRepository;
            this._bookRepository = bookRepository;
            this._orderItemRepository = orderItemRepository;
        }

        public async Task<IActionResult> Index()
            => View(await _orderRepository.GetAllSortedByOrderPlacedDescAsync());

        public async Task<IActionResult> Details(int id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);

            if (order == null) return RedirectToAction(nameof(Index));

            IEnumerable<OrderItem> orderItems = order.OrderItems;

            return View(orderItems);
        }

        public async Task<IActionResult> Create()
        {
            Order order = new Order
            {
                OrderPlaced = DateTime.UtcNow,
                OrderItems = await this.GetOrderItemsWithAssignedIdAsyc()
            };

            await _orderRepository.AddAsync(order);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Private utility methods --------------------------------------------

        private async Task<IEnumerable<OrderItem>> GetAllOrderItemsFromCartContentAsync()
        {
            // TODO: make GetById asynchronous and add await here
            // CartItem inherits from the OrderItem
            IEnumerable<OrderItem> orderItems = _cartRepository.GetAllCartItems();

            // TODO: Try to find a better place to clear the shopping cart
            _cartRepository.ClearCart();

            return orderItems;
        }

        private async Task<IList<OrderItem>> GetOrderItemsWithAssignedIdAsyc()
        {
            IList<OrderItem> orderItemsWithId = new List<OrderItem>();

            foreach (OrderItem orderItem in await this.GetAllOrderItemsFromCartContentAsync())
            {
                orderItemsWithId.Add(await _orderItemRepository.AddAsync(orderItem));
            }

            return orderItemsWithId;
        }
    }
}
