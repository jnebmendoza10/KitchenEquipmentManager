using KitchenEquipmentManager.Domain.Models;

namespace KitchenEquipmentManager.Infrastructure.Services.Users
{
    public interface IAuthenticationService
    {
        bool Register(User user);
        User Login(string username, string password);
    }
}
