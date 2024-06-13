using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechTest.DTO;
using TechTest.Services.Interface;

namespace TechTest.Controllers
{
    [ApiController]
    [Route("api")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUsers(UsersDTO users)
        {
            if (!users.IsValid())
            {
                return BadRequest("User Not Valid");
            }

            users.Role = "UserAccess";
            await _userService.InsertAsync(users);

            return Created();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
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
    }
}