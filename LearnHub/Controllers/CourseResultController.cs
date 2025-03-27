using LearnHub.Models;
using LearnHub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseResultController : ControllerBase
    {
        private readonly LearnHubContext context;
        public CourseResultController(LearnHubContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CourseResult> courseResults = context.CourseResults.ToList();
            return Ok(courseResults);
        }

        [HttpPost]
        public IActionResult AddCourseResult(CourseResult courseResult)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Course CourseExists = context.Courses.SingleOrDefault(c => c.Id == courseResult.CourseId);
                if (CourseExists == null)
                {
                    return BadRequest("Invalid CourseId , this Course does not exist.");
                }

                context.CourseResults.Add(courseResult);
                context.SaveChanges();

                return Ok(new { Message = "Result Was Added Successfully}" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Database error occurred: " + ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
