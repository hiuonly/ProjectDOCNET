using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void AddProduct(Product product) => ProductDao.Instance.AddProduct(product);

        public Product CheckProductByName(string name) => ProductDao.Instance.CheckProductByName(name);

        public void DeleteProduct(Product product) => ProductDao.Instance.DeleteProduct(product);

        public IEnumerable<Product> GetProductByBrandId(int brandId) => ProductDao.Instance.GetProductByBrandId(brandId);

        public Product GetProductById(int id) => ProductDao.Instance.GetProductById(id);

        public IEnumerable<Product> GetProductByName(string name) => ProductDao.Instance.GetProductByName(name);
        public IEnumerable<Product> GetProducts() => ProductDao.Instance.GetProducts();

        public IEnumerable<Product> GetProductTop3() => ProductDao.Instance.GetProductTop3();

        public void UpdateProduct(Product product) => ProductDao.Instance.UpdateProduct(product);
    }
}
