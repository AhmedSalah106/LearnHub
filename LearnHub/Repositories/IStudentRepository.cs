using LearnHub.Models;

namespace LearnHub.Repositories
{
    public interface IStudentRepository
    {
        Student GetById(int Id);
        List<Student> GetAll();
        void Insert(Student student);
        void Update(Student student);
        void Delete(Student student);
        void Save();
    }
}