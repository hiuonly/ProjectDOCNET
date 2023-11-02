using BusinessObject.Context;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CartDao
    {
        public static CartDao instance;
        public static readonly object instanceLock = new object();
        public static CartDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CartDao();
                    }
                }
                return instance;
            }
        }
        public IEnumerable<Cart> GetCarts()
        {
            List<Cart> carts;
            try
            {
                var shopContext = new ShopContext();
                carts = (from c in shopContext.Carts
                         join u in shopContext.Users on c.userId equals u.userId
                         join p in shopContext.Products on c.productId equals p.productId
                         select new Cart
                         {
                             cartId = c.cartId,
                             productId= c.productId,
                             userId = c.userId,
                             quantity = c.quantity,
                             status = c.status,
                             Products = p,
                             Users= u
                         }).ToList();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return carts;
        }
        public IEnumerable<Cart> GetCartsSell()
        {
            List<Cart> carts;
            try
            {
                var shopContext = new ShopContext();
                carts = (from c in shopContext.Carts
                         join u in shopContext.Users on c.userId equals u.userId
                         join p in shopContext.Products on c.productId equals p.productId
                         join b in shopContext.Brands on p.brandId equals b.brandId
                         where c.status != 0
                         select new Cart
                         {
                             cartId = c.cartId,
                             productId = c.productId,
                             userId = c.userId,
                             quantity = c.quantity,
                             status = c.status,
                             Products = new Product
                             {
                                 productId = p.productId,
                                 pImg = p.pImg,
                                 productName = p.productName,
                                 productDescription = p.productDescription,
                                 brandId = p.brandId,
                                 price = p.price,
                                 quantity = p.quantity,
                                 Brands = b
                             },
                             Users = u
                         }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return carts;
        }
        public IEnumerable<Cart> GetUseInCarts(int uid)
        {
            List<Cart> carts;
            try
            {
                var shopContext = new ShopContext();
                carts = (from c in shopContext.Carts
                         join u in shopContext.Users on c.userId equals u.userId
                         join p in shopContext.Products on c.productId equals p.productId
                         where c.userId  == uid && c.status == 0
                         select new Cart
                         {
                             cartId = c.cartId,
                             productId = c.productId,
                             userId = c.userId,
                             quantity = c.quantity,
                             status = c.status,
                             Products = p,
                             Users = u
                         }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return carts;
        }
        public IEnumerable<Cart> GetUseMyOrder(int uid)
        {
            List<Cart> carts;
            try
            {
                var shopContext = new ShopContext();
                carts = (from c in shopContext.Carts
                         join u in shopContext.Users on c.userId equals u.userId
                         join p in shopContext.Products on c.productId equals p.productId
                         where c.userId == uid && c.status != 0
                         select new Cart
                         {
                             cartId = c.cartId,
                             productId = c.productId,
                             userId = c.userId,
                             quantity = c.quantity,
                             status = c.status,
                             Products = p,
                             Users = u
                         }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return carts;
        }
        public Cart CartDetail(int id)
        {
            Cart cart = null;
            try
            {
                var shopContext = new ShopContext();
                cart = (from c in shopContext.Carts
                        join u in shopContext.Users on c.userId equals u.userId
                        join p in shopContext.Products on c.productId equals p.productId
                        where c.cartId == id
                        select new Cart
                        {
                            cartId = c.cartId,
                            productId = c.productId,
                            userId = c.userId,
                            quantity = c.quantity,
                            status = c.status,
                            Products = p,
                            Users = u
                        }).SingleOrDefault();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cart;
        }
        public Cart checkCart(int userId, int productId)
        {
            Cart cart = null;
            try
            {
                var shopContext = new ShopContext();
                cart = (from c in shopContext.Carts
                        join u in shopContext.Users on c.userId equals u.userId
                        join p in shopContext.Products on c.productId equals p.productId
                        where c.productId == productId && c.userId == userId && c.status == 0
                        select new Cart
                        {
                            cartId = c.cartId,
                            productId = c.productId,
                            userId = c.userId,
                            quantity = c.quantity,
                            status = c.status,
                            Products = p,
                            Users = u
                        }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cart;
        }
        public void AddCart(Cart cart)
        {
            try
            {
                var shopContext = new ShopContext();
                shopContext.Carts.Add(cart);
                shopContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateCart(Cart cart)
        {
            try
            {
                var card = CartDetail(cart.cartId);
                if(card != null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Entry<Cart>(cart).State = EntityState.Modified;
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The cart does not already exist.");
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void pay(pay cart)
        {
            try
            {
                var card = CartDetail(cart.cartId);
                if (card != null)
                {
                    var shopContext = new ShopContext();
                    var pay = new Cart
                    {
                        cartId = cart.cartId,
                        status = cart.status
                    };
                    shopContext.Entry<Cart>(pay).State = EntityState.Modified;
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The cart does not already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void DeleteCart(Cart cart)
        {
            try
            {
                var card = CartDetail(cart.cartId);
                if (card != null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Remove(cart);
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The cart does not already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
