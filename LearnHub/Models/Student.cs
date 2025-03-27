using System.ComponentModel.DataAnnotations.Schema;

namespace LearnHub.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageURl {  get; set; }
        public string Address {  get; set; }
        public string Grade {  get; set; }
        public string Email {  get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public List<CourseResult>? CourseResults { get; set; }
        public Department? Department { get; set; }

    }
}
