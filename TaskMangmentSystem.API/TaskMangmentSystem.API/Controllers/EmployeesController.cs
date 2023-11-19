using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskMangmentSystem.API.Dtos.EmployeeDto;
using TaskMangmentSystem.API.Errors;
using TaskMangmentSystem.Core.Entities;
using TaskMangmentSystem.Core.Interfaces;

namespace TaskMangmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
            if (employee is null) return NotFound(new ApiResponse(404, $"Not found With Id {id}"));
            return _mapper.Map<Employee, GetEmployeeDto>(employee);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetEmployeeDto>>> GetEmployess()
        {
            var employees = await _unitOfWork.Repository<Employee>().ListAllAsync();
            return _mapper.Map<List<Employee>, List<GetEmployeeDto>>(employees.ToList());
        }
        
        [HttpPost("add")]
        public async Task<ActionResult<GetEmployeeDto>> AddAsync([FromBody] EmployeeDto employeeDto)
        {
            var employeeToAdd = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email
            };

            var addedEmployee = await _unitOfWork.Repository<Employee>().AddAsync(employeeToAdd);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return BadRequest(new ApiResponse(400, $"Something Wrong happened While Adding"));
            return _mapper.Map<Employee, GetEmployeeDto>(addedEmployee);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<GetEmployeeDto>> UpdateAsync(int id, [FromBody] EmployeeDto employeeDto)
        {
            var getEmployee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
            if (getEmployee is null) return NotFound(new ApiResponse(404, $"Not found With Id {id}"));

            getEmployee.Name = employeeDto.Name;
            getEmployee.Email = employeeDto.Email;

            var updatedEmployee = _unitOfWork.Repository<Employee>().Update(getEmployee);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return BadRequest(new ApiResponse(400, $"Something Wrong happened While Updating"));
            return _mapper.Map<Employee, GetEmployeeDto>(updatedEmployee);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<GetEmployeeDto>> DeleteAsync(int id)
        {
            var getEmployee = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
            if (getEmployee is null) return NotFound(new ApiResponse(404, $"Not found With Id {id}"));

            var deletedEmployee = _unitOfWork.Repository<Employee>().Delete(getEmployee);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return BadRequest(new ApiResponse(400, $"Something Wrong happened While Deleting"));
            return _mapper.Map<Employee, GetEmployeeDto>(deletedEmployee);
        }
    }
}
