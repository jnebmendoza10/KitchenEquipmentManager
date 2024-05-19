using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManager.Domain.Models;

namespace KitchenEquipmentManager.Infrastructure.Services.Users
{
    public interface IUserService
    {
        List<User> GetAllUsers(string role);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
    }
}
