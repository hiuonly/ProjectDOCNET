using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> GetBrands();
        Brand GetBrandById(int id);
        Brand GetBrandByName(string name);
        void AddBrand(Brand brand);
        void Update(Brand brand);
        void Delete(Brand brand);
    }
}
