using FoodDelivery.Api.DAL.Common.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Api.DAL.Common.Entities.Auth;

public class AppUserEntity : IdentityUser<Guid>, IEntity
{
    public Guid Id { get; init; }

    public AppUserEntity()
    {
        Id = Guid.NewGuid();
    }
}