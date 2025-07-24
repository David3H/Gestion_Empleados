using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Repositories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllStoreAsync();
        Task<Store> GetStoreByIdAsync(int id);
        Task AddStoreAsync(Store store);
        Task UpdateStoreAsync(Store store);
        Task DeleteStoreAsync(int id);
    }
}
