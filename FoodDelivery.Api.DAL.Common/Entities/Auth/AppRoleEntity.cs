using FoodDelivery.Api.DAL.Common.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Api.DAL.Common.Entities.Auth;

public class AppRoleEntity : IdentityRole<Guid>, IEntity
{
    public Guid Id { get; init; }
}