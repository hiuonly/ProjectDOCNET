using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BrandRepository : IBrandRepository
    {
        public void AddBrand(Brand brand) => BrandDao.Instance.AddBrand(brand);

        public void Delete(Brand brand) => BrandDao.Instance.Delete(brand);

        public Brand GetBrandById(int id) => BrandDao.Instance.GetBrandById(id);

        public Brand GetBrandByName(string name) => BrandDao.Instance.GetBrandByName(name);

        public IEnumerable<Brand> GetBrands() => BrandDao.Instance.GetBrands();

        public void Update(Brand brand) => BrandDao.Instance.Update(brand);
    }
}
