using SealMarket.Core.Entities;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;
using SealMarket.Core.Constants;
using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Interfaces;

namespace SealMarket.Infrastructure.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
       public NotificationRepository(AppDbContext context) : base(context) { }

        public async Task<List<Notification>> GetNotificationsAsync(NotificationsFilter filter)
        {
            var query = _context.Notifications.AsQueryable();

            query = query
                .Where(n => n.Name.Contains(filter.SearchText));

            query = query
                .Where(n => n.HasBeenRead == filter.HasBeenRead);
                
            query = query
                .Where(n => n.DateTime >= filter.FromDateTime && n.DateTime <= filter.ToDateTime);

            query = filter.OrderParam switch
            {
                NotificationOrderParams.DateTime => filter.ByAscending
                    ? query.OrderBy(n => n.DateTime)
                    : query.OrderByDescending(n => n.DateTime),

                _ => filter.ByAscending
                    ? query.OrderBy(n => n.Name)
                    : query.OrderByDescending(n => n.Name),
            };

            query = query
                .Skip(filter.Size * (filter.Page - 1))
                .Take(filter.Size);

            return await query.ToListAsync();
        }
    }
}
