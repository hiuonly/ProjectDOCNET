using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectPRN231API.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjectPRN231API.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository repository = new UserRepository();
        private readonly AppSettings _appSettings;
        public UserController(IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _appSettings = optionsMonitor.CurrentValue;
        }
        [HttpGet]
        [Route("listUser")]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            try
            {
                return Ok(repository.GetUsers());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("login")]
        public ActionResult Login(Login login)
        {
            try
            {
                var user = repository.Login(login.email, login.password);
                if (user == null)
                {
                    return Ok(new ApiReponse
                    {
                        Success = false,
                        Message = "Invalid username/pass"
                    });
                }
                var isAdmin = CheckIfUserIsAdmin(user);
                var token = GenerateToken(user, isAdmin);
                return Ok(new ApiReponse
                {
                    Success = true,
                    Message = "Authenticate success",
                    Data = token
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        private bool CheckIfUserIsAdmin(User user)
        {
            // You can implement your own logic to check if the user is an admin
            // Here we simply check if the user's email ends with a certain domain
            if (user.roleId == 1)
            {
                return true;
            }
            return false;
        }
        private string GenerateToken(User user, bool isAdmin)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.ScretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.firstName + " " + user.lastName),
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim("UserName", user.lastName),
                    new Claim("Id", user.userId.ToString()),

                    //roles

                    new Claim("TokenId", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }
        [HttpGet]
        [Route("UserInfor")]
        public ActionResult<User> UserInformation(int id)
        {
            try
            {
                var user = repository.GetUserById(id);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetUserByRole")]
        public ActionResult<IEnumerable<User>> GetUGetUserByRoleser(int id)
        {
            try
            {
                return Ok(repository.GetUsersRole(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public ActionResult Rigister(CUser user)
        {
            try
            {
                if(user == null) return NotFound();
                var register = new User
                {
                    uImg = user.uImg,
                    email = user.email,
                    password = user.password,
                    firstName= user.firstName,
                    lastName = user.lastName,
                    phoneNumber = user.phoneNumber,
                    roleId = user.roleId,
                    address = user.address
                };
                repository.AddUser(register);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public ActionResult UpdateUser(EUser user)
        {
            try
            {
                var euser = new User
                {
                    userId = user.userId,
                    uImg = user.uImg,
                    email = user.email,
                    password = user.password,
                    firstName = user.firstName,
                    lastName = user.lastName,
                    phoneNumber = user.phoneNumber,
                    roleId = user.roleId,
                    address = user.address
                };
                repository.UpdateUser(euser);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        public ActionResult RemoveUser(int id)
        {
            try
            {
                var user = repository.GetUserById(id);
                if (user == null)
                    return NotFound();
                repository.RemoveUser(user);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
