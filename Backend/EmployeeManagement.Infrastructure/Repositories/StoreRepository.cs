using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Repositories;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class StoreRepository(AppDbContext appDbContext) : IStoreRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;
        public async Task AddStoreAsync(Store store)
        {
            _appDbContext.Store.Add(store);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteStoreAsync(int id)
        {
            var store = await _appDbContext.Store.FindAsync(id);
            if (store != null)
            {
                _appDbContext.Store.Remove(store);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Store>> GetAllStoreAsync() =>
            await _appDbContext.Store.ToListAsync();

        public async Task<Store> GetStoreByIdAsync(int id) =>
            await _appDbContext.Store.FindAsync(id);

        public async Task UpdateStoreAsync(Store store)
        {
           _appDbContext.Store.Update(store);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
