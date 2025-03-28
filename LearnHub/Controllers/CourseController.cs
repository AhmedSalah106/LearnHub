using LearnHub.Models;
using LearnHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly LearnHubContext context;
        public CourseController(LearnHubContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Course> courses = context.Courses.ToList();
            return Ok(courses);
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                context.Courses.Add(course);
                context.SaveChanges();

                return Ok(new { Message = $"Couse {course.Name} was added Successfully" });
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
        public IActionResult UpdateCourse(int Id, Course UpdatedCourse)
        {

            try
            {

                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Course CourseExists = context.Courses.FirstOrDefault(c => c.Id == Id);

                if(CourseExists == null)
                {
                    return NotFound($"Course With Id {Id} Not Found");
                }

                CourseExists.Name = UpdatedCourse.Name;
                CourseExists.MinDegree = UpdatedCourse.MinDegree;
                CourseExists.Degree = UpdatedCourse.Degree;

                context.SaveChanges();

                return Ok(new {Message = $"Course {CourseExists.Name} Was Successfully Updated"});

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteCourse(int Id)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Course CourseExists = context.Courses.FirstOrDefault(C => C.Id == Id);
                if (CourseExists == null)
                {
                    return NotFound($"No Course With Id: {CourseExists.Id} Founded");
                }

                context.Courses.Remove(CourseExists);
                context.SaveChanges();

                return Ok(new { Message = $"Course {CourseExists.Name} Was Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
