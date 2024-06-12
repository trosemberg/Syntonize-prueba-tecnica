using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TechTest.Authentication;
using TechTest.DTO;
using TechTest.Models;
using TechTest.Services;
using TechTest.Services.Interface;


namespace TechTest.Controllers
{
    [RoutePrefix("api/roles")]
    [JwtAuthAdmin]
    public class RolesController : ApiController
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetRolesAsycn()
        {
            var message = RequestContext.Principal.Identity.Name;
            var roles = await _rolesService.GetAllAsync();
            if (roles.Any())
                return Ok(roles);

            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetRoles(int id)
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
        public async Task<IHttpActionResult> PutRoles(int id, [FromBody] RolesDTO roles)
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostRoles([FromBody] RolesDTO roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _rolesService.InsertAsync(roles);

            return StatusCode(HttpStatusCode.Created);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteRoles(int id)
        {
            var roles = await _rolesService.DeleteAsync(id);
            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}