using LearnApi.Models;
using LearnApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnApi.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
            => _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {
            var filePath = Path.Combine("Storage", employeeView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeView.Photo.CopyTo(fileStream);

            var employee = new Employee(employeeView.Name, employeeView.Age, filePath);

            _employeeRepository.Add(employee);

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var employees = _employeeRepository.GetAll();

            return Ok(employees);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/download")]
        public ActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee is null) return NotFound();

            var dataBytes = System.IO.File.ReadAllBytes(employee.Photo);

            return File(dataBytes, "image/png");
        }
    }
}

