using System;
using System.Collections.Generic;
using System.Linq;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Constants;
using KitchenEquipmentManager.Repository.Services;

namespace KitchenEquipmentManager.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IDataRepository<User> _userRepository;
        public UserService(IDataRepository<User> userRepository) 
        { 
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public bool DeleteUser(User user)
        {
            try
            {
                _userRepository.Remove(user.Id);

                return true; // Successful delete
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<User> GetAllUsers(string role)
        {
            if (role == RoleType.ADMIN)
            {
                return new List<User>(); // return an empty list since we will not have access to Users tab
            }

            try
            {
                var users = _userRepository.GetAll();

                return users.ToList();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _userRepository.Update(user, user.Id);

                return true; // Successful delete
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
