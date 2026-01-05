using System;
using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.UpdateDTOs
{
    public record UpdateUserDto
    (
        [MinLength(2)]
        [MaxLength(120)]
        string? Name = null,

        [DataType(DataType.Date)]
        DateOnly? BirthDate = null,

        [MaxLength(50)]
        string? City = null,

        [Url]
        [MaxLength(500)]
        string? AvatarUrl = null
    );
}
