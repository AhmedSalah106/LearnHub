using LearnHub.Models;
using LearnHub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        [HttpPost("Add")]
        public IActionResult AddDepartment(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Departments.Add(department);
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }


        [HttpPost("Update")]
        public IActionResult UpdateDepartment(int Id, Department UpdatedDepartment)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Department Department = context.Departments.FirstOrDefault(x => x.Id == Id);

                if(Department == null)
                {
                    return NotFound("Department Not Founded");
                }

                Department.Name = UpdatedDepartment.Name;   
                Department.ManagerName = UpdatedDepartment.ManagerName;
                
                context.SaveChanges();

                return Ok(new {Message = $"Department {Department.Name} Was Updated Successfully"});
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
