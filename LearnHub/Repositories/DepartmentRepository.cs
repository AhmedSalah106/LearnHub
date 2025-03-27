using LearnHub.Models;
using LearnHub.Services;

namespace LearnHub.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly LearnHubContext context;
        public DepartmentRepository(LearnHubContext context)
        {
            this.context = context;
        }
        public void Delete(Department department)
        {
            context.Departments.Remove(department);
        }

        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public Department GetById(int Id)
        {
            return context.Departments.FirstOrDefault(e=>e.Id==Id);
        }

        public void Insert(Department department)
        {
            context.Departments.Add(department);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Department department)
        {
            context.Departments.Update(department);
        }
    }
}
