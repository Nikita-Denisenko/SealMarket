using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.NotificationDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;

namespace SealMarket.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo)
        {
            _repo = repo; 
        }

        public Task<CreatedNotificationDto> CreateNotificationAsync(CreateNotificationDto createNotificationDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNotificationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationDto> GetNotificationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ShortNotificationDto>> GetNotificationsAsync(NotificationsFilterDto notificationsFilterDto)
        {
            throw new NotImplementedException();
        }
    }
}
