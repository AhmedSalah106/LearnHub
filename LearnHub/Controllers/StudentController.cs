using LearnHub.Models;
using LearnHub.Repositories;
using LearnHub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentRepository studentRepository;
        private readonly IDepartmentRepository departmentRepository;
        public StudentController(IStudentRepository studentRepository
            , IDepartmentRepository departmentRepository )
        {
            this.studentRepository = studentRepository;
            this.departmentRepository = departmentRepository;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = studentRepository.GetAll();

            return Ok(students);
        }


        [HttpPost("Add")]
        public IActionResult AddStudent(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Department DepartmentExists = departmentRepository.GetById(student.DepartmentId);
                    if (DepartmentExists == null)
                    {
                        return BadRequest("Invalid DepartmentId , this Department does not exist.");
                    }
                    studentRepository.Insert(student);
                    studentRepository.Save();
                    return Ok(new { Message = $"Studnet {student.FirstName} {student.LastName} Added Successfully" });
                }
                return BadRequest(ModelState);


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


        [HttpPost("Update")]
        public IActionResult UpdateStudent(int Id, Student UpdatedStudent)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Student student = studentRepository.GetById(Id);

                if(student == null)
                {
                    return NotFound("Student Not Founded");
                }

                student.FirstName = UpdatedStudent.FirstName;
                student.LastName = UpdatedStudent.LastName;
                student.Email = UpdatedStudent.Email;
                student.Address = UpdatedStudent.Address;

                studentRepository.Update(student);
                studentRepository.Save();


                return Ok(new {Message = $"Student {student.FirstName} {student.LastName} was Updated Successfully"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }


        }

        
    }
}
