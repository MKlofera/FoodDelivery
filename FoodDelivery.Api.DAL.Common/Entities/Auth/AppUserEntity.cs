using FoodDelivery.Api.DAL.Common.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Api.DAL.Common.Entities.Auth;

public class AppUserEntity : IdentityUser<Guid>
{
    public string? PhoneNumberToClosePerson { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? DateOfBirth { get; set; }

}