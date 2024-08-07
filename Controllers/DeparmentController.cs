using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Context;
using PerformanceSurvey.Models;

namespace PerformanceSurvey.Controllers
{

    [Route("api/Department/")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;

        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(ApplicationDbContext context, ILogger<DepartmentController> logger)
        {
            _Context = context;
            _logger = logger;
        }

        //https://localhost:7164/api/Department/Create
        [HttpPost("Create")]

        public async Task<ActionResult<Department>> CreateDepartment(DepartmentDto request)
        {
            _logger.LogInformation("creating a new Department with  Name{DepartmentName}", request.DepartmentName);

            Department department = new Department() {

                DepartmentId = request.DepartmentId,
                DepartmentName = request.DepartmentName,
                CreatedAt = DateTime.UtcNow,

            };

            try
            {
                department.CreatedAt = DateTime.UtcNow;
                _Context.departments.Add(department);
                await _Context.SaveChangesAsync();

                _logger.LogInformation("department with ID{DepartmentId} created successfully", department.DepartmentId);

                return CreatedAtAction("GetDepartment", new { department = department.DepartmentId }, department);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new Department.");
                return StatusCode(500, "Internal server error");
            }


        }

        //get request https://localhost:7164/api/Department/get/{id}

        [HttpGet("get/{id}")]

        public async Task<ActionResult<Department>> GetDepartment(int id)

        {
            var department = await _Context.departments.FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
            {
                return NotFound("not Found");
            }

            return department;
        }


        //update  https://localhost:7164/api/Department/update /{id}
        [HttpPut("update /{id}")]
        public async Task<ActionResult<Department>> UpdateDepartment( int id, DepartmentDto departmentDto)
        {
            try
            {
                var department = await _Context.departments.FindAsync(id);

                if (department == null)
                {
                    return NotFound("Department does not exist");
                }

                department.DepartmentName = departmentDto.DepartmentName;
                department.UpdatedAt = DateTime.UtcNow;

                _Context.Entry(department).State = EntityState.Modified;

                await _Context.SaveChangesAsync();
                return StatusCode(200, department);

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        //getAll Request https://localhost:7164/api/Department/getAll
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {

            var departments = await _Context.departments.ToListAsync();

            return departments;

        }


        //Delete Request https://localhost:7164/api/Department/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {

            var department = await _Context.departments.FindAsync(id);

            if(department == null)
            {
                return NotFound("Department Not found");
            }

            _Context.Remove(department);

            await _Context.SaveChangesAsync();

            return StatusCode(200, department);

        }



    }
}
