using Microsoft.EntityFrameworkCore;
using SealMarket.Infrastructure.Data;


namespace SealMarket.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context) { }
    }
}
