using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [Route("users/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext dbContext;

        public UserController(DatabaseContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            if (dbContext.Users.IsNullOrEmpty())
                return BadRequest("User list is empty.");

            return Ok(await dbContext.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> Get(int id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user == null)
                return BadRequest("User not found.");
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> Post(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Ok(await dbContext.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> Put(User updated)
        {
            var dbUser = await dbContext.Users.FindAsync(updated.id);
            if (dbUser == null)
                return BadRequest("User not found.");

            dbUser.username = updated.username;

            await dbContext .SaveChangesAsync();

            return Ok(await dbContext.Users.ToListAsync());
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<List<User>>> Delete(int id)
        {
            var dbUser = await dbContext.Users.FindAsync(id);
            if (dbUser == null)
                return BadRequest("User not found.");

            dbContext.Users.Remove(dbUser);
            await dbContext.SaveChangesAsync();

            return Ok(await dbContext.Users.ToListAsync());
        }


    }
}
