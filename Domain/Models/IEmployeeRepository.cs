namespace LearnApi.Domain.Models;

public interface IEmployeeRepository
{
    void Add(Employee employee);
    List<EmployeeDTO> GetAll(int pageNumber, int pageQuantity);
    Employee? Get(int id);
}