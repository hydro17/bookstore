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
            List<int> productIdList = this.GetProductIdList();
            productIdList.Add(productId);
            this.SetProductIdList(productIdList);
        }

        public IEnumerable<CartItem> GetAllCartItemsSorteAscBy<ReturnType>(Func<CartItem, ReturnType> sortBy)
        {
            IEnumerable<CartItem> orderItems = this.GetProductIdList()
                .GroupBy(
                bookId => bookId, // bookId as key
                bookId => bookId, // collection of bookId as value
                (key_bookId, value_bookIdCollection) => new CartItem
                {
                    Book = _bookRepository.GetById(key_bookId),
                    Quantity = value_bookIdCollection.Count()
                })
                .OrderBy(sortBy);

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

        public void ClearCart()
            => _session.Set<List<int>>(SD.ShoppingCart, new List<int>());

        public List<int> GetProductIdList()
            => _session.Get<List<int>>(SD.ShoppingCart) ?? new List<int>();

        private void SetProductIdList(List<int> shoppingCarList)
            => _session.Set<List<int>>(SD.ShoppingCart, shoppingCarList);
    }
}
