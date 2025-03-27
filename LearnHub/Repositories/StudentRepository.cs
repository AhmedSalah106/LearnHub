using LearnHub.Models;
using LearnHub.Services;

namespace LearnHub.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly LearnHubContext context;
        public StudentRepository(LearnHubContext context) 
        {
            this.context = context;
        }
        public void Delete(Student student)
        {
            context.Students.Remove(student);
        }

        public List<Student> GetAll()
        {
            List<Student> Students = context.Students.ToList();
            return Students;
        }

        public Student GetById(int Id)
        {
            Student Student = context.Students.FirstOrDefault(x => x.Id == Id);

            return Student;
        }

        public void Insert(Student student)
        {
            context.Students.Add(student);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Student student)
        {
            context.Students.Update(student);
        }
    }
}
