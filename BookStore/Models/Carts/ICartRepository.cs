using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Carts
{
    public interface ICartRepository
    {
        IEnumerable<CartItem> GetAllCartItemsSortedAscBy<ReturnType>(Func<CartItem, ReturnType> sortBy);
        CartItem GetCartItemById(int cartItemId);
        void AddToCart(int cartItemId);
        bool RemoveFromCart(int cartItemId);
        void ClearCart();
        IEnumerable<CartItem> GetAllCartItems();
        List<int> GetBookIdListOrEmptyBookIdList();
    }
}
