namespace LearnApi.Models;

public interface IEmployeeRepository
{
    void Add(Employee employee);
    List<Employee> GetAll();
    Employee? Get(int id);
}