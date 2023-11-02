using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectPRN231API.DTO;

namespace ProjectPRN231API.Controller
{
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandRepository repository = new BrandRepository();
        [HttpGet]
        public ActionResult<IEnumerable<Brand>> GetBrand()
        {
            try
            {
                return Ok(repository.GetBrands());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("id")]
        public ActionResult<Brand> GetBrandId(int id)
        {
            try
            {
                return Ok(repository.GetBrandById(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("name")]
        public ActionResult<Brand> GetBrandName(string name)
        {
            try
            {
                return Ok(repository.GetBrandByName(name));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public ActionResult AddBrand(CBrand brand)
        {
            try
            {
                var check = repository.GetBrandByName(brand.brandName);
                if (check != null)
                    return BadRequest();
                var b = new Brand
                {
                    brandName = brand.brandName,
                    bImg = brand.bImg
                };
                repository.AddBrand(b);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public ActionResult UpdateBrand(UBrand brand)
        {
            try
            {
                var check = repository.GetBrandById(brand.brandId);
                if (check == null)
                    return NotFound();
                var b = new Brand
                {
                    brandId = brand.brandId,
                    brandName = brand.brandName,
                    bImg = brand.bImg
                };
                repository.Update(b);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        public ActionResult DeleteBrand(int id)
        {
            try
            {
                var check = repository.GetBrandById(id);
                if (check == null)
                    return NotFound();
                repository.Delete(check);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
