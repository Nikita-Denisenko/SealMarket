using SealMarket.Core.Entities;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces.Repositories
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        public Task<List<Notification>> GetNotificationsAsync(NotificationsFilter filter);
    }
}
