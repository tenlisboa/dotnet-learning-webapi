namespace LearnApi.Models;

public interface IEmployeeRepository
{
    void Add(Employee employee);
    List<Employee> GetAll(int pageNumber, int pageQuantity);
    Employee? Get(int id);
}