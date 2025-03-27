using LearnHub.Models;
using LearnHub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly LearnHubContext context;
        public InstructorController(LearnHubContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Instructor> Instructors = context.Instructors.ToList();
            return Ok(Instructors);
        }
        [HttpPost]
        public IActionResult AddInstructor(Instructor instructor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                context.Add(instructor);
                context.SaveChanges();

                return Ok(new { Message = $"instructor {instructor.FirstName} {instructor.LastName} was added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
