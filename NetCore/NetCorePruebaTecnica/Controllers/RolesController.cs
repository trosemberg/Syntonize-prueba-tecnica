using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TechTest.DTO;
using TechTest.Services.Interface;


namespace TechTest.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetRolesAsycn()
        {
            var roles = await _rolesService.GetAllAsync();
            if (roles.Any())
                return Ok(roles);

            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRoles(int id)
        {
            var role = await _rolesService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutRoles(int id, [FromBody] RolesDTO roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roles.Id)
            {
                return BadRequest("Body Id and Request Id are different.");
            }
            var check = await _rolesService.UpdateAsync(roles);

            if (check != null)
                return BadRequest("Failed to update");

            return Created();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostRoles([FromBody] RolesDTO roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _rolesService.InsertAsync(roles);

            return Created();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRoles(int id)
        {
            var roles = await _rolesService.DeleteAsync(id);
            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        private bool isAdmin() 
        {
            // Cast to ClaimsIdentity.
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;

            // Gets name from claims. Generally it's an email address.
            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Role)
                .FirstOrDefault();

            return usernameClaim?.Value.Equals("Admin") ?? false;
        }
    }
}