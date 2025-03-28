﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LearnHub.Models
{
    public class Course
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        [ForeignKey("Department")]
        public int DepartmntId { get; set; }
        public Department? Department { get; set; }
        public List<Instructor>? Instructors { get; set; }
        public List<CourseResult>? CourseResults { get; set;}
    }
}
