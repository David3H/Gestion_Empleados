using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Repositories;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class EmployeeRepository(AppDbContext appDbContext) : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task AddEmployeeAsync(Employee employee)
        {
            _appDbContext.Employees.Add(employee);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeAsync(int id)
        {
           var employee = await _appDbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                _appDbContext.Employees.Remove(employee);
                await _appDbContext.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync() =>
            await _appDbContext.Employees.ToListAsync();

        public async Task<Employee> GetEmployeeByIdAsync(int id) =>
           await _appDbContext.Employees.FindAsync(id);

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _appDbContext.Employees.Update(employee);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
