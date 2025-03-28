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

        [HttpPost("Add")]
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

        [HttpPut("Update")]
        public IActionResult UpdateCourseResult(int Id,CourseResult UpdatedCourseResult)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                CourseResult CourseExists = context.CourseResults.FirstOrDefault(c => c.Id == Id);

                if (CourseExists == null)
                {
                    return NotFound($"Course Result With Id {Id} NSot Found");
                }

                CourseExists.Id = UpdatedCourseResult.Id;
                CourseExists.Degree = UpdatedCourseResult.Degree;

                context.SaveChanges();

                return Ok(new { Message = $"Course Result With Id : {CourseExists.Id} Was Successfully Updated" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteCourseResult(int Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                CourseResult CourseResultExists = context.CourseResults.FirstOrDefault(C => C.Id == Id);

                if (CourseResultExists == null)
                {
                    return NotFound($"No CourseResult With Id : {Id}");
                }

                context.CourseResults.Remove(CourseResultExists);
                context.SaveChanges();

                return Ok($"Course Result With Id :  {Id} Was Deleted Successfully");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
