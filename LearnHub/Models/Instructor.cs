using System.ComponentModel.DataAnnotations.Schema;

namespace LearnHub.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImageURL {  get; set; }
        public decimal Salary {  get; set; }
        public string Address {  get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

    }
}
