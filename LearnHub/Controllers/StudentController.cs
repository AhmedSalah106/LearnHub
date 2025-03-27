﻿using LearnHub.Models;
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
            try
            {
                if (ModelState.IsValid)
                {
                    Department DepartmentExists = context.Departments.FirstOrDefault(x => x.Id == student.DepartmentId);
                    if (DepartmentExists == null)
                    {
                        return BadRequest("Invalid DepartmentId , this Department does not exist.");
                    }
                    context.Students.Add(student);
                    context.SaveChanges();
                    return Ok(new { Message = "Studnet Added Successfully" });
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
    }
}
