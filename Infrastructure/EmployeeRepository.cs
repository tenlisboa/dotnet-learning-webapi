using LearnApi.Models;

namespace LearnApi.Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionContext _context;

        public EmployeeRepository(ConnectionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public Employee? Get(int id)
        {
            return _context.Employees.Find(id);
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
    }
}