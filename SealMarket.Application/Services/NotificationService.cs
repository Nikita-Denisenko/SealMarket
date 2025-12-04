using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Interfaces.Services;

namespace SealMarket.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo)
        {
            _repo = repo; 
        }
    }
}
