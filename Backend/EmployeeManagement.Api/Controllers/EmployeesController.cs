using AutoMapper;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper) : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeeRepository.GetAllEmployeeAsync();
            var employeesDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _employeeRepository.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(Get), new {id = employee.Id}, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EmployeeDto dto)
        {
            var existing = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existing == null) return NotFound();
            _mapper.Map(dto, existing);
            await _employeeRepository.UpdateEmployeeAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeRepository.DeleteEmployeAsync(id);
            return NoContent();
        }
    }
}
