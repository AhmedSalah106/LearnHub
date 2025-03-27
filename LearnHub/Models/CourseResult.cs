using System.ComponentModel.DataAnnotations.Schema;

namespace LearnHub.Models
{
    public class CourseResult
    {
        public int Id { get; set; }
        public int Degree {  get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId {  get; set; }
        public Student? Student { get; set; }
        public Course? Course {  get; set; }
    }
}
