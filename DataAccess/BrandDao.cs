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
    public class BrandDao
    {
        public static BrandDao instance;
        public static readonly object instanceLock = new object();
        public static BrandDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BrandDao();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Brand> GetBrands()
        {
            List<Brand> brands;
            try
            {
                var shopContext = new ShopContext();
                brands = shopContext.Brands.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return brands;
        }
        public Brand GetBrandById(int id)
        {
            Brand brand = null;
            try
            {
                var shopContext = new ShopContext();
                brand = shopContext.Brands.SingleOrDefault(b => b.brandId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return brand;
        }
        public Brand GetBrandByName(string name)
        {
            Brand brand = null;
            try
            {
                var shopContext = new ShopContext();
                brand = shopContext.Brands.SingleOrDefault(b => b.brandName == name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return brand;
        }
        public void AddBrand(Brand brand)
        {
            try
            {
                var name = GetBrandByName(brand.brandName);
                if (name == null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Brands.Add(brand);
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The brand already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Brand brand)
        {
            try
            {
                var id = GetBrandById(brand.brandId);
                if (id != null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Entry<Brand>(brand).State = EntityState.Modified;
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The Brand does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(Brand brand)
        {
            try
            {
                var id = GetBrandById(brand.brandId);
                if (id != null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Brands.Remove(brand);
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The Brand does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
