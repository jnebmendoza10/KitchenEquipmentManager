using System.Collections.Generic;
using KitchenEquipmentManager.Domain.Models;

namespace KitchenEquipmentManager.Infrastructure.Services.Users
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
