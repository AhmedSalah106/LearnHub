using LearnHub.Models;
using LearnHub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly LearnHubContext context;
        public StudentController(LearnHubContext context) 
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index() 
        {
            List<Student> students = context.Students.ToList();
           
            return Ok(students);
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                context.Students.Add(student);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

    }
}
