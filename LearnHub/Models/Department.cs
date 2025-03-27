using System.ComponentModel.DataAnnotations.Schema;

namespace LearnHub.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName {  get; set; }
       
        public List<Course>? Courses { get;  set; }
        public List<Student>? Students { get; set; }
        public List<Instructor>? Instructors { get; set; }

    }
}
