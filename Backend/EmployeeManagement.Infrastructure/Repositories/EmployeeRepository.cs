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
            _appDbContext.Employee.Add(employee);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeAsync(int id)
        {
           var employee = await _appDbContext.Employee.FindAsync(id);
            if (employee != null)
            {
                _appDbContext.Employee.Remove(employee);
                await _appDbContext.SaveChangesAsync();

            }
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync() =>
            await _appDbContext.Employee.ToListAsync();

        public async Task<Employee> GetEmployeeByIdAsync(int id) => 
            await _appDbContext.Employee
             .Include(e => e.Store)
             .FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _appDbContext.Employee.Update(employee);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
