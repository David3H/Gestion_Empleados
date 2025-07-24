using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; };
        public DbSet<Store> Stores { get; set; };
    }
}
