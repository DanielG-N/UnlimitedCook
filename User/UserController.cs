using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserDB _db;

        public UserController(ILogger<UserController> logger, UserDB db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("addUser")]
        public async Task<IResult> AddUser(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Results.Created($"/{user.Id}", user);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _db.Users.FindAsync(id);

            if(user == null)
                return NotFound();
            
            return Ok(user);
        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _db.Users.ToListAsync();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IResult> DeleteUser(int id)
        {
            if(await _db.Users.FindAsync(id) is User user)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return Results.Ok(user);
            }

            return Results.NotFound();
        }

        [HttpPut]
        [Route("updateUser/{id}")]
        public async Task<IResult> updateUser(User user1)
        {
            var user2 = await _db.Users.FindAsync(user1.Id);

            if(user2 == null)
                return Results.NotFound();
            
            user2.name = user1.name;
            user2.username = user1.username;
            user2.password = user1.password;

            await _db.SaveChangesAsync();

            return Results.NoContent();

        }
    }

    

}
