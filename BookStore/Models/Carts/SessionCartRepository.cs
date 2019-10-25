using BookStore.Extensions;
using BookStore.Models.Books;
using BookStore.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Carts
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
            List<int> productIdList = this.GetBookIdListOrEmptyBookIdList();
            productIdList.Add(productId);
            this.SetProductIdList(productIdList);
        }

        public IEnumerable<CartItem> GetAllCartItemsSortedAscBy<ReturnType>(Func<CartItem, ReturnType> sortBy)
            => GetAllCartItems().OrderBy(sortBy);

        public bool RemoveFromCart(int bookId)
        {
            List<int> bookIdList = this.GetBookIdListOrEmptyBookIdList();

            if (bookIdList.Count > 0)
            {
                bookIdList.Remove(bookId);
                this.SetProductIdList(bookIdList);

                return true;
            }

            return false;
        }

        public CartItem GetCartItemById(int cartItemId)
            => this.GetAllCartItems().FirstOrDefault(cartItem => cartItem.Book.Id == cartItemId);

        public IEnumerable<CartItem> GetAllCartItems()
        {
            IEnumerable<CartItem> cartItems = this.GetBookIdListOrEmptyBookIdList()
                .GroupBy(
                    bookId => bookId,
                    bookId => bookId,
                    (key_bookId, value_bookIdCollection) => new CartItem
                        {
                            Book = _bookRepository.GetById(key_bookId),
                            Quantity = value_bookIdCollection.Count()
                        }
                );

            return cartItems;
        }

        public void ClearCart()
            => _session.Set<List<int>>(SD.ShoppingCart, new List<int>());

        public List<int> GetBookIdListOrEmptyBookIdList()
            => _session.Get<List<int>>(SD.ShoppingCart) ?? new List<int>();

        private void SetProductIdList(List<int> shoppingCarList)
            => _session.Set<List<int>>(SD.ShoppingCart, shoppingCarList);
    }
}
