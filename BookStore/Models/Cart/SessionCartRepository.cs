using BookStore.Extensions;
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

        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly ISession _session;

        //private List<int> ShoppingCartList
        //{
        //    get => _httpContextAccessor.HttpContext.Session.Get<List<int>>("shoppingCart") ?? new List<int>();
        //    set => _httpContextAccessor.HttpContext.Session.Set<List<int>>("shoppingCart", value);
        //}

        public SessionCartRepository(IBookRepository bookRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._session = httpContextAccessor.HttpContext.Session;
            this._bookRepository = bookRepository;
        }

        public void AddToCart(int productId)
        {
            List<int> productIdList = GetProductIdList();
            productIdList.Add(productId);
            SetProductIdList(productIdList);
        }

        public IEnumerable<OrderItem> GetAllCartItems()
        {
            IEnumerable<OrderItem> orderItems = GetProductIdList()
                .GroupBy(
                bookId => bookId,
                bookId => bookId,
                (bookId, bookIdCollection) => new OrderItem
                {
                    Book = _bookRepository.GetById(bookId),
                    Quantity = bookIdCollection.Count()
                });

            return orderItems;
        }

        public bool RemoveFromCart(int productId)
        {
            List<int> productIdList = GetProductIdList();

            if (productIdList.Count > 0)
            {
                productIdList.Remove(productId);
                SetProductIdList(productIdList);

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
