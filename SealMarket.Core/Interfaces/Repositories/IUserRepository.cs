using SealMarket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SealMarket.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task SaveChangesAsync();
        public Task<List<User>> GetAllAsync();
        public Task<User?> GetByIdAsync(int id);
        public Task AddAsync(User user);
        public Task DeleteByIdAsync(int id);
        public Task UpdateUser(User user);
    }
}

