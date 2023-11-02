using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CartRepository : ICartRepository
    {
        public void AddCart(Cart cart) => CartDao.Instance.AddCart(cart);

        public Cart CartDetail(int id) => CartDao.Instance.CartDetail(id);

        public Cart checkCart(int userId, int productId) => CartDao.Instance.checkCart(userId, productId);

        public void DeleteCart(Cart cart) => CartDao.Instance.DeleteCart(cart);

        public IEnumerable<Cart> GetCarts() => CartDao.Instance.GetCarts();

        public IEnumerable<Cart> GetCartsSell() => CartDao.Instance.GetCartsSell();

        public IEnumerable<Cart> GetUseInCarts(int uid) => CartDao.Instance.GetUseInCarts(uid);

        public IEnumerable<Cart> GetUseMyOrder(int uid) => CartDao.Instance.GetUseMyOrder(uid);

        public void pay(pay cart) => CartDao.Instance.pay(cart);
        public void UpdateCart(Cart cart) => CartDao.Instance.UpdateCart(cart);
    }
}
