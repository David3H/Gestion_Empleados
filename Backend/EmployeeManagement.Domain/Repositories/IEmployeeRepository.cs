using EmployeeManagement.Domain.Entities;


namespace EmployeeManagement.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeeAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeAsync(int id);
    }
}
