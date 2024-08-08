using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Context;
using PerformanceSurvey.Models;



namespace PerformanceSurvey.Controllers

{

    [Route("api/user/")]
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
        //https://localhost:5008/api/user/create
        // POST: api/User/create
        [HttpPost("create")]
        public async Task<ActionResult<User>> CreateUser(UserRequest request)
        {


            _logger.LogInformation("Creating a new user with email {UserEmail}.", request.UserEmail);

            User user = new User()
            {

               
                name = request.Name, 
                 createdAt = DateTime.UtcNow,
                    password = request.Password,
                     userEmail = request.UserEmail,
                      userType = request.UserType, 

                   
            };
            try
            {
                user.createdAt = DateTime.UtcNow;
                _context.users.Add(user);
                await _context.SaveChangesAsync();



                _logger.LogInformation("User with ID {UserID} created successfully.", user.id);
                return CreatedAtAction("GetUser", new { id = user.id }, user);
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
            var user = await _context.users.Include(u => u.departmentId)
                                           .FirstOrDefaultAsync(u => u.id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT:https://localhost:7164/api/Users/update/4
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutUser(int id, UserRequest userDto)
        {

            try
            {
                var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.name = userDto.Name;
            user.userEmail = userDto.UserEmail;
            user.password = userDto.Password; // Consider hashing the password
            user.userType = userDto.UserType;
            user.updatedAt = DateTime.UtcNow;

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
            var users = await _context.users.Include(u => u.departmentId).ToListAsync();

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
            return _context.users.Any(e => e.id == id);
        }
         public async Task EnsureUserExistsAsync(string email, User newUser)
    {
        var user = await _context.users.SingleOrDefaultAsync(u => u.userEmail == email);

        if (user == null)
        {
            _logger.LogInformation("User with email {Email} not found. Creating a new user.", email);
            await RegisterUserAsync(newUser);
        }
        else
        {
            _logger.LogInformation("User with email {Email} already exists.", email);
        }
    }

  private async Task RegisterUserAsync(User user)
    {
        try
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User with email {Email} registered successfully.", user.userEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering a new user with email {Email}.", user.userEmail);
            throw;
        }
    }

    }

}
