using BusinessObject.Context;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDao
    {
        public static ProductDao instance;
        public static readonly object instanceLock = new object();
        public static ProductDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDao();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Product> GetProducts()
        {
            List<Product> products;
            try
            {
                var shopContext = new ShopContext();
                products = (from p in shopContext.Products
                            join c in shopContext.Brands on p.brandId equals c.brandId
                            select new Product
                            {
                                productId = p.productId,
                                pImg = p.pImg,
                                productName = p.productName,
                                productDescription = p.productDescription,
                                brandId = p.brandId,
                                Brands = c,
                                price = p.price,
                                quantity = p.quantity
                            }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }
        public IEnumerable<Product> GetProductByBrandId(int brandId)
        {
            List<Product> products;
            try
            {
                var shopContext = new ShopContext();
                products = (from p in shopContext.Products
                            join c in shopContext.Brands on p.brandId equals c.brandId
                            where p.brandId == brandId
                            select new Product
                            {
                                productId = p.productId,
                                pImg = p.pImg,
                                productName = p.productName,
                                productDescription = p.productDescription,
                                brandId = p.brandId,
                                Brands = c,
                                price = p.price,
                                quantity = p.quantity
                            }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }
        public Product GetProductById(int id)
        {
            Product product = null;
            try
            {
                var shopContext = new ShopContext();
                product = (from p in shopContext.Products
                           join c in shopContext.Brands on p.brandId equals c.brandId
                           where p.productId == id
                           select new Product
                           {
                               productId = p.productId,
                               pImg = p.pImg,
                               productName = p.productName,
                               productDescription = p.productDescription,
                               brandId = p.brandId,
                               Brands = c,
                               price = p.price,
                               quantity = p.quantity
                           }).SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }
        public Product CheckProductByName(string name)
        {
            Product product = null;
            try
            {
                var shopContext = new ShopContext();
                product = (from p in shopContext.Products
                           join c in shopContext.Brands on p.brandId equals c.brandId
                           where p.productName == name
                           select new Product
                           {
                               productId = p.productId,
                               pImg = p.pImg,
                               productName = p.productName,
                               productDescription = p.productDescription,
                               brandId = p.brandId,
                               Brands = c,
                               price = p.price,
                               quantity = p.quantity
                           }).SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }
        public IEnumerable<Product> GetProductByName(string name)
        {
            List<Product> product = null;
            try
            {
                var shopContext = new ShopContext();
                product = (from p in shopContext.Products
                           join c in shopContext.Brands on p.brandId equals c.brandId
                           where p.productName.Contains(name)
                           select new Product
                           {
                               productId = p.productId,
                               pImg = p.pImg,
                               productName = p.productName,
                               productDescription = p.productDescription,
                               brandId = p.brandId,
                               Brands = c,
                               price = p.price,
                               quantity = p.quantity
                           }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }
        public IEnumerable<Product> GetProductTop3()
        {
            List<Product> products = null;
            try
            {
                var shopContext = new ShopContext();
                products = (from p in shopContext.Products
                             join c in shopContext.Brands on p.brandId equals c.brandId
                             orderby p.productId descending
                             select new Product
                             {
                                 productId = p.productId,
                                 pImg = p.pImg,
                                 productName = p.productName,
                                 productDescription = p.productDescription,
                                 brandId = p.brandId,
                                 Brands = c,
                                 price = p.price,
                                 quantity = p.quantity
                             }).Take(4).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return products;
        }
        public void AddProduct(Product product)
        {
            try
            {
                var name = CheckProductByName(product.productName);
                if (name == null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Products.Add(product);
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The product already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateProduct(Product product)
        {
            try
            {
                var p = GetProductById(product.productId);
                if (p != null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Entry<Product>(product).State = EntityState.Modified;
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void DeleteProduct(Product product)
        {
            try
            {
                var p = GetProductById(product.productId);
                if (p != null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Products.Remove(product);
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
