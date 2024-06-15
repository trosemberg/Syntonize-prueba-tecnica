using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechTest.DTO;
using TechTest.Services.Interface;

namespace TechTest.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _userService.GetAllAsync();
            if (users.Any())
                return Ok(users);

            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var users = await _userService.GetByIdAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutUsersAsync(int id, [FromBody] UsersDTO users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!users.IsValid())
            {
                return BadRequest("User Not Valid");
            }

            if (id != users.Id)
            {
                return BadRequest("Body Id and Request Id are different.");
            }

            var check = await _userService.UpdateAsync(users);

            if (check != null)
                return BadRequest("Failed to update");

            return Created();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostUsers(UsersDTO users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!users.IsValid())
            {
                return BadRequest("User Not Valid");
            }

            users.RoleId = 2;
            await _userService.InsertAsync(users);

            return Created();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await _userService.DeleteAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
    }
}