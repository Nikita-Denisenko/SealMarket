using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;
using static SealMarket.Core.Constans.UserOrderParameters;


namespace SealMarket.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<List<User>> GetUsersAsync(UsersFilter filter)
        {
            var query = _context.Users.AsQueryable();

            query = query
                .Where(u => u.Name.Contains(filter.SearchText));

            var minBirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-filter.MaxAge - 1));
            var maxBirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-filter.MinAge));

            query = query.Where(u => u.BirthDate >= minBirthDate && u.BirthDate <= maxBirthDate);


            query = filter.OrderParam switch
            {
                BirthDate => filter.ByAscending
                    ? query.OrderBy(u => u.BirthDate)
                    : query.OrderByDescending(u => u.BirthDate),

                _ => filter.ByAscending
                    ? query.OrderBy(u => u.Name)
                    : query.OrderByDescending(u => u.Name),
            };

            query = query
                .Skip(filter.Size * (filter.Page - 1))
                .Take(filter.Size);

            return await query.ToListAsync();
        }
    }
}
