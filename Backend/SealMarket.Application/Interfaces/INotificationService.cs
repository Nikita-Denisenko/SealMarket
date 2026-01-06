using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.NotificationDtos;

namespace SealMarket.Application.Interfaces
{
    public interface INotificationService
    {
        public Task<List<ShortNotificationDto>> GetNotificationsAsync(NotificationsFilterDto notificationsFilterDto, int? AccountId);
        public Task<NotificationDto> GetNotificationAsync(int id);
        public Task<CreatedNotificationDto> CreateNotificationAsync(CreateNotificationDto createNotificationDto);
        public Task DeleteNotificationAsync(int id);
    }
}
