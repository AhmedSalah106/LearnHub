using LearnHub.Models;
using LearnHub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly LearnHubContext context;

        public DepartmentController(LearnHubContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Department> departments = context.Departments.ToList();
            return Ok(departments);
        }

        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                context.Departments.Add(department);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
