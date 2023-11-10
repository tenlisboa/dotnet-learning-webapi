using LearnApi.Domain.Models;
using LearnApi.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace LearnApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/employee")]
    [ApiVersion("1.0", Deprecated = true)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {
            _logger.LogDebug("Request", employeeView);

            var filePath = Path.Combine("Storage", employeeView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeView.Photo.CopyTo(fileStream);

            var employee = new Employee(employeeView.Name, employeeView.Age, filePath);

            _employeeRepository.Add(employee);

            _logger.LogDebug("Employee Added Successfully");

            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var employee = _employeeRepository.Get(id);

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employeeDTO);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll(int pageNumber, int pageQuantity)
        {
            var employees = _employeeRepository.GetAll(pageNumber, pageQuantity);
            var employeesDTO = _mapper.Map<List<EmployeeDTO>>(employees);

            return Ok(employeesDTO);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/download")]
        public ActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee is null) return NotFound();

            // unsafe, fixed in v2
            var dataBytes = System.IO.File.ReadAllBytes(employee!.Photo!);

            return File(dataBytes, "image/png");
        }
    }
}

