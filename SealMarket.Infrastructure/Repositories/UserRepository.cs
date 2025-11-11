using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Entities;
using SealMarket.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SealMarket.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
            => await _context.Users.ToListAsync();

        public async Task<User?> GetByIdAsync(int id)
            => await _context.Users.FindAsync(id);

        public async Task AddAsync(User user) 
        {
            await _context.Users.AddAsync(user);
            await SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var userToDelete = await GetByIdAsync(id);

            if (userToDelete is null)
                throw new Exception("User is not found.");

            _context.Users.Remove(userToDelete);
            await SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await SaveChangesAsync();
        }
    }
}
