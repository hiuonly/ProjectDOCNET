using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICartRepository
    {
        IEnumerable<Cart> GetCarts();
        IEnumerable<Cart> GetUseInCarts(int uid);
        Cart CartDetail(int id);
        Cart checkCart(int userId, int productId);
        void AddCart(Cart cart);
        void UpdateCart(Cart cart);
        void DeleteCart(Cart cart);
        IEnumerable<Cart> GetCartsSell();
        void pay(pay cart);
        IEnumerable<Cart> GetUseMyOrder(int uid);
    }
}
