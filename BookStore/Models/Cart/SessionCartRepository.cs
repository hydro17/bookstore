using BookStore.Extensions;
using BookStore.Models.Cart;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class SessionCartRepository : ICartRepository
    {
        private readonly IBookRepository _bookRepository;
        private readonly ISession _session;

        public SessionCartRepository(IBookRepository bookRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._session = httpContextAccessor.HttpContext.Session;
            this._bookRepository = bookRepository;
        }

        public void AddToCart(int productId)
        {
            List<int> productIdList = this.GetProductIdList();
            productIdList.Add(productId);
            this.SetProductIdList(productIdList);
        }

        public IEnumerable<CartItem> GetAllCartItemsSortedByTitle()
        {
            IEnumerable<CartItem> orderItems = this.GetProductIdList()
                .GroupBy(
                bookId => bookId,
                bookId => bookId,
                (bookId, bookIdCollection) => new CartItem
                {
                    Book = _bookRepository.GetById(bookId),
                    Quantity = bookIdCollection.Count()
                })
                .OrderBy(cartItem => cartItem.Book.Title);

            return orderItems;
        }

        public bool RemoveFromCart(int productId)
        {
            List<int> productIdList = this.GetProductIdList();

            if (productIdList.Count > 0)
            {
                productIdList.Remove(productId);
                this.SetProductIdList(productIdList);

                return true;
            }

            return false;
        }

        private List<int> GetProductIdList()
            => _session.Get<List<int>>("shoppingCart") ?? new List<int>();

        private void SetProductIdList(List<int> shoppingCarList)
            => _session.Set<List<int>>("shoppingCart", shoppingCarList);
    }
}
