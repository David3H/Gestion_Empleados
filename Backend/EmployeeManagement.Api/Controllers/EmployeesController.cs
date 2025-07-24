using AutoMapper;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(IEmployeeRepository employeeRepository, IStoreRepository storeRepository, IMapper mapper) : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IStoreRepository _storeRepository = storeRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeeAsync();
                return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error al obtener listado de empleado");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null) return NotFound();
                return Ok(_mapper.Map<EmployeeDto>(employee));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error al obtener el empleado específico");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto dto)
        {
            var storeExists = await _storeRepository.GetStoreByIdAsync(dto.StoreId);
            if (storeExists == null) return BadRequest("La tienda especificada no existe");

            var employee = _mapper.Map<Employee>(dto);
            employee.Store = null;
      
            try 
            {
                await _employeeRepository.AddEmployeeAsync(employee);
                return CreatedAtAction(nameof(GetById), new { id = employee.Id }, _mapper.Map<EmployeeDto>(employee));
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "Error al crear el empleado");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la ruta no coincide con el ID del empleado");

            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
                return NotFound("Empleado no encontrado");

            var storeExists = await _storeRepository.GetStoreByIdAsync(dto.StoreId);
            if (storeExists == null)
                return BadRequest("La tienda especificada no existe");

            existingEmployee.FirstName = dto.FirstName;
            existingEmployee.LastName = dto.LastName;
            existingEmployee.Email = dto.Email;
            existingEmployee.Position = dto.Position;
            existingEmployee.HireDate = dto.HireDate;
            existingEmployee.Active = dto.Active;
            existingEmployee.StoreId = dto.StoreId;

            try
            {
                await _employeeRepository.UpdateEmployeeAsync(existingEmployee);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error al actualizar el empleado");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeRepository.DeleteEmployeAsync(id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error al eliminar el empleado");
            }
        }
    }
}
