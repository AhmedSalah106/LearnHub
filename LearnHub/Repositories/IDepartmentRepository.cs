using LearnHub.Models;

namespace LearnHub.Repositories
{
    public interface IDepartmentRepository
    {
        Department GetById(int Id);
        List<Department> GetAll();
        void Insert(Department department);
        void Update(Department department);
        void Delete(Department department);
        void Save();
    }
}