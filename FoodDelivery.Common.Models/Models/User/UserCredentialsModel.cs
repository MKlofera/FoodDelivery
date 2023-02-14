using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Common.Models.Models.User;

public class UserCredentialsModel
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}