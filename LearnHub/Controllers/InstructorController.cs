using LearnHub.Models;
using LearnHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LearnHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
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

        [HttpPost("Add")]
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

        [HttpPut("Update")]
        public IActionResult UpdateInstructor(int Id, Instructor UpdatedInstructor)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Instructor InstructorExists = context.Instructors.FirstOrDefault(I => I.Id == Id);

                if (InstructorExists == null)
                {
                    return NotFound($"Instructor With Id {Id} Not Found");
                }

                InstructorExists.FirstName = UpdatedInstructor.FirstName;
                InstructorExists.LastName = UpdatedInstructor.LastName;
                InstructorExists.Address = UpdatedInstructor.Address;
                InstructorExists.Email = UpdatedInstructor.Email;
                InstructorExists.DepartmentId = UpdatedInstructor.DepartmentId;
                InstructorExists.CourseId = UpdatedInstructor.CourseId;

                context.SaveChanges();

                return Ok(new { Message = $"Instructor {InstructorExists.FirstName} {InstructorExists.LastName} Was Successfully Updated" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteInstructor(int Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Instructor InstructorExists = context.Instructors.FirstOrDefault(d => d.Id == Id);

                if (InstructorExists == null)
                {
                    return NotFound($"No Instructor With Id : {Id}");
                }

                context.Instructors.Remove(InstructorExists);
                context.SaveChanges();

                return Ok($"Department {InstructorExists.FirstName} {InstructorExists.LastName} Was Deleted Successfully");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

    }
}
