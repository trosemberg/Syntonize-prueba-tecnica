using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TechTest.Authentication;
using TechTest.DTO;
using TechTest.Services.Interface;

namespace TechTest.Controllers
{
    [RoutePrefix("api/users")]
    [JwtAuthAdmin]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetUsersAsync()
        {
            var users = await _userService.GetAllAsync();
            if (users.Any())
                return Ok(users);

            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetUsers(int id)
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
        public async Task<IHttpActionResult> PutUsersAsync(int id, [FromBody] UsersDTO users)
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostUsers(UsersDTO users)
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteUsers(int id)
        {
            var users = await _userService.DeleteAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}