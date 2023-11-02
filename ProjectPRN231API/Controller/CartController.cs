using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectPRN231API.DTO;

namespace ProjectPRN231API.Controller
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartRepository repository = new CartRepository();
        [HttpGet]
        public ActionResult<Cart> GetCarts()
        {
            try
            {
                return Ok(repository.GetCarts());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("MyOrder")]
        public ActionResult<Cart> GetMyOrder()
        {
            try
            {
                return Ok(repository.GetCartsSell());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("cartDetail")]
        public ActionResult<Cart> CartDetail(int id)
        {
            try
            {
                var c = repository.CartDetail(id);
                if(c == null)
                    return NotFound();
                return Ok(c);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("cartUser")]
        public ActionResult<IEnumerable<Cart>> GetCartUser(int id)
        {
            try
            {
                var u = repository.GetUseInCarts(id);
                if(u == null)
                    return NotFound();
                return Ok(u);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("myorderuser")]
        public ActionResult<IEnumerable<Cart>> myorderuser(int id)
        {
            try
            {
                var u = repository.GetUseMyOrder(id);
                if (u == null)
                    return NotFound();
                return Ok(u);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public ActionResult AddCart(ACart cart)
        {
            try
            {
                var existingCart = repository.checkCart(cart.userId, cart.productId);

                if (existingCart == null)
                {
                    var newCart = new Cart
                    {
                        userId = cart.userId,
                        productId = cart.productId,
                        quantity = cart.quantity,
                        status = 0
                    };
                    repository.AddCart(newCart);
                }
                else
                {
                    existingCart.quantity += cart.quantity;
                    repository.UpdateCart(existingCart);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding item to cart: {ex.Message}");
            }
        }
        [HttpPut]
        public ActionResult PayCart(UCart cart)
        {
            try
            {
                var check = repository.CartDetail(cart.cartId);
                if (check == null)
                    return NotFound();
                var c = new Cart
                {
                    cartId = cart.cartId,
                    userId = cart.userId,
                    productId = cart.productId,
                    quantity = cart.quantity,
                    status = cart.status
                };
                repository.UpdateCart(c);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        public ActionResult DeleteCart(int id)
        {
            try
            {
                var check = repository.CartDetail(id);
                if (check == null)
                    return NotFound();
                repository.DeleteCart(check);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
