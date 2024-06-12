using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TechTest.DTO;
using TechTest.Services.Interface;

namespace TechTest.Controllers
{
    [RoutePrefix("api")]
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> RegisterUsers(UsersDTO users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!users.IsValid())
            {
                return BadRequest("User Not Valid");
            }

            users.Role = "UserAccess";
            await _userService.InsertAsync(users);

            return StatusCode(HttpStatusCode.Created);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!login.IsValid())
            {
                return BadRequest("User Not Valid");
            }
            string jwt;
            try
            {
                jwt = await _userService.LoginUserAsync(login);
                if (string.IsNullOrEmpty(jwt))
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return Unauthorized();
                // TODO configure Logger and log
            }

            return Ok(jwt);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}