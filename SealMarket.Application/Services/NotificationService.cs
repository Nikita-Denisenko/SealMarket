using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.NotificationDtos;
using SealMarket.Application.Helpers;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo)
        {
            _repo = repo; 
        }

        public async Task<CreatedNotificationDto> CreateNotificationAsync(CreateNotificationDto createNotificationDto)
        {
            if (createNotificationDto is null)
                throw new ArgumentNullException(nameof(createNotificationDto));

            var notification = new Notification
            (
                createNotificationDto.AccountId, 
                createNotificationDto.Message, 
                createNotificationDto.Name
            );

            await _repo.AddAsync(notification);

            return new CreatedNotificationDto
            (
                notification.Id,
                notification.AccountId
            );
        }

        public async Task DeleteNotificationAsync(int id)
        {
            if (!await _repo.ExistsAsync(id))
                throw new KeyNotFoundException($"Notification with id {id} not found.");

            await _repo.DeleteByIdAsync(id);
            await _repo.SaveChangesAsync();
        }

        public async Task<NotificationDto> GetNotificationAsync(int id)
        {
            var notification = await _repo.GetByIdAsync(id);

            if (notification is null)
                throw new KeyNotFoundException($"Notification with id {id} not found.");

            return new NotificationDto
            (
                notification.Id,
                notification.Name,
                notification.Message,
                notification.DateTime,
                notification.HasBeenRead,
                notification.AccountId
            );
        }

        public async Task<List<ShortNotificationDto>> GetNotificationsAsync(NotificationsFilterDto notificationsFilterDto, int? AccountId)
        {
            if (notificationsFilterDto is null)
                throw new ArgumentNullException(nameof(notificationsFilterDto));

            var filter = new NotificationsFilter
            (
                notificationsFilterDto.Page,
                notificationsFilterDto.Size,
                notificationsFilterDto.FromDateTime ?? DateTimeHelper.MinDateToFilter,
                notificationsFilterDto.ToDateTime ?? DateTimeHelper.MaxDateToFilter,
                notificationsFilterDto.HasBeenRead,
                notificationsFilterDto.OrderParam,
                notificationsFilterDto.ByAscending,
                notificationsFilterDto.SearchText
            );

            var notifications = await _repo.GetNotificationsAsync(filter, AccountId);

            return notifications.Select(notification => new ShortNotificationDto
            (
                notification.Id,
                notification.Name,
                notification.DateTime,
                notification.HasBeenRead
            )).ToList();
        }
    }
}
