using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceSurvey.Context;
using PerformanceSurvey.Models;

namespace PerformanceSurvey.Controllers
{

    [Route("api/Question/")]
    [ApiController]
    public class DepartmentQuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _Context;
        private readonly ILogger<DepartmentQuestionController> _Logger;

        public DepartmentQuestionController(ApplicationDbContext context, ILogger<DepartmentQuestionController> logger)
        {
            _Context = context;
            _Logger = logger;

        }
        //create request https://localhost:7164/api/Question/Create

        [HttpPost("Create")]
        public async Task<ActionResult<DepartmentQuestion>> CreateQuestion(DepartmentQuestionDto questionDto)
        {
            _Logger.LogInformation("Creating a new question with  {QuestionId}", questionDto.QuestionId);

            DepartmentQuestion question = new DepartmentQuestion()
            {

                QuestionText = questionDto.QuestionText,
                DepartmentId = questionDto.DepartmentId,
                CreatedAt = DateTime.UtcNow,

                Options = questionDto.Options?.Select(o => new DepartmentQuestionOption
                {
                    Text = o.Text,
                    Score = o.Score
                }).ToList() ?? new List<DepartmentQuestionOption>()

            };

            try
            {

                question.CreatedAt = DateTime.UtcNow;
                _Context.department_questions.AddAsync(question);
                await _Context.SaveChangesAsync();

                _Logger.LogInformation("Created question with {QuestionId}", questionDto.QuestionId);

                //return CreatedAtAction("GetQuestion", new { question = question.QuestionId }, question);
                return Ok(question);

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "An Error occured while creating question");
                return BadRequest(ex.Message);

            }
        }


        //get request https://localhost:7164/api/Question/get/{id}

        [HttpGet("get/{id}")]

        public async Task<ActionResult<DepartmentQuestion>> GetDepartmentQuestion(int id)
        {
            // Fetch the department question along with its options
            var departmentQuestion = await _Context.department_questions
                .Include(d => d.Options)          // Include related options
                .FirstOrDefaultAsync(d => d.QuestionId == id);

            // Check if the department question was found
            if (departmentQuestion == null)
            {
                return NotFound();
            }

            // Return the department question with its options
            return Ok(departmentQuestion);
        }


        //get request https://localhost:7164/api/Question/update/{id}

        [HttpPut("update/{id}")]
        
        public async Task <ActionResult<DepartmentQuestion>> UpdateDepartmentQuestions(int id, DepartmentQuestionDto questionDto)
        {
            try
            {
                // Retrieve the question and its related options
                var question = await _Context.department_questions
                    .Include(q => q.Options)
                    .FirstOrDefaultAsync(q => q.QuestionId == id);

                if (question == null)
                {
                    return NotFound();
                }

                // Update the properties of the question
                question.QuestionText = questionDto.QuestionText;
                question.DepartmentId = questionDto.DepartmentId;
                question.CreatedAt = DateTime.UtcNow;

                // Handle existing options
                var existingOptionIds = question.Options.Select(o => o.OptionId).ToList();

                foreach (var option in question.Options.ToList())
                {
                    if (!questionDto.Options.Any(o => o.OptionId == option.OptionId))
                    {
                        // Remove options that are no longer present in the DTO
                        _Context.Remove(option);
                    }
                    else
                    {
                        // Update existing options
                        var updatedOption = questionDto.Options.First(o => o.OptionId == option.OptionId);
                        option.Text = updatedOption.Text;
                        option.Score = updatedOption.Score;
                    }
                }

                // Add new options
                foreach (var newOptionDto in questionDto.Options.Where(o => o.OptionId == 0))
                {
                    var newOption = new DepartmentQuestionOption
                    {
                        Text = newOptionDto.Text,
                        Score = newOptionDto.Score,
                        QuestionId = id
                    };
                    question.Options.Add(newOption);
                }

                _Context.Entry(question).State = EntityState.Modified;

                await _Context.SaveChangesAsync();

                return Ok(question);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, ex.Message);
            }


        }


        //get all https://localhost:7164/api/Question/getAll
        [HttpGet("getAll")]

        public async Task <ActionResult<DepartmentQuestion>> GetAllDepartmentQuestion(int id)
        {
            var questions = await _Context.department_questions
                    .Include(q => q.Options) // Eager load the Options related to each DepartmentQuestion
                    .ToListAsync();

            return Ok(questions);


        }

        //Delete https://localhost:7164/api/Question/delete/{id}

        [HttpDelete ("delete/{id}")]

        public async Task <ActionResult<DepartmentQuestion>> DeleteDepartmentQuestion(int id)
        {
            var delete = await _Context.department_questions
        .Include(q => q.Options) 
        .FirstOrDefaultAsync(q => q.QuestionId == id);
            if (delete == null)
            {
                return NotFound();
            }
            _Context.Remove(delete);

            await _Context.SaveChangesAsync();
            return Ok(delete);

        }




    }
}
