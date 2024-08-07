using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Context;
using PerformanceSurvey.Models;



namespace PerformanceSurvey.Controllers

{

    [Route("api/Users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;
        public UserController(ApplicationDbContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;

        }
        //https://localhost:7164/api/Users/Create
        // POST: api/Users/Create
        [HttpPost("Create")]
        public async Task<ActionResult<User>> CreateUser(UserDto
         request)
        {


            _logger.LogInformation("Creating a new user with email {UserEmail}.", request.UserEmail);

            User user = new User()
            {

                UserId = request.UserId,
                Name = request.Name, 
                 CreatedAt = DateTime.UtcNow,
                   DepartmentId = request.DepartmentId,
                    Password = request.Password,
                     UserEmail = request.UserEmail,
                      UserType = request.UserType, 

                   
            };
            try
            {
                user.CreatedAt = DateTime.UtcNow;
                _context.users.Add(user);
                await _context.SaveChangesAsync();



                _logger.LogInformation("User with ID {UserID} created successfully.", user.UserId);
                return CreatedAtAction("GetUser", new { id = user.UserId }, user);
                //return StatusCode(200, "this is ok");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new user.");
                return StatusCode(500, "Internal server error");
            }
            
        }


        // GET:https://localhost:7164/api/Users/get/{id}
        [HttpGet("get/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.users.Include(u => u.Department)
                                           .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT:https://localhost:7164/api/Users/update/4
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto userDto)
        {

            try
            {
                var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Name = userDto.Name;
            user.UserEmail = userDto.UserEmail;
            user.Password = userDto.Password; // Consider hashing the password
            user.UserType = userDto.UserType;
            user.UpdatedAt = DateTime.UtcNow;

             _context.Entry(user).State = EntityState.Modified;
           
                await _context.SaveChangesAsync();
                return StatusCode(200, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                
            }

    }


        // GET:https://localhost:7164/api/Users/getAll
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.users.Include(u => u.Department).ToListAsync();

            return users;
        }

        // DELETE: http://localhost:5000/api/Users/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.users.FindAsync(id);


            if (user == null)
            {
                return NotFound();
            }
            _context.users.Remove(user);
            //user.DeletedAt = DateTime.UtcNow;
            //_context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return StatusCode(200, "");
        }


        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.UserId == id);
        }
    }

}
