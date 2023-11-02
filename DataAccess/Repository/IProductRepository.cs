using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProductByBrandId(int brandId);
        Product GetProductById(int id);
        Product CheckProductByName(string name);
        IEnumerable<Product> GetProductByName(string name);
        IEnumerable<Product> GetProductTop3();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
