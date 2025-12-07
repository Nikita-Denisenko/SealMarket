using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.EntityDtos;

namespace SealMarket.Application.Interfaces
{
    public interface INotificationService
    {
        public Task<List<ReadNotificationDto>> GetAllNotificationsAsync();
        public Task<List<ReadNotificationDto>> GetNotificationsAsync(NotificationsFilterDto notificationsFilterDto);
        public Task<ReadNotificationDto> GetNotificationAsync(int id);
        public Task<CreatedNotificationDto> CreateNotificationAsync(CreateNotificationDto createNotificationDto);
        public Task DeleteNotificationAsync(int id);
    }
}
