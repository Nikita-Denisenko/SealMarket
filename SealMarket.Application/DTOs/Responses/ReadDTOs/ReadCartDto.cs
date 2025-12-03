using SealMarket.Core.Entities;

namespace SealMarket.Application.DTOs.Responses.EntityDtos
{
    public record ReadCartDto
    (
        int Id,
        string Name,
        int AccountId,
        List<int> ProductsIds
    );
}
