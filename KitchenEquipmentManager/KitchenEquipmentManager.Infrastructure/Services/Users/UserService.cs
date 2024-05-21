using System;
using System.Collections.Generic;
using System.Linq;
using KitchenEquipmentManager.Domain.Models;
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
        public void DeleteUser(User user)
        {
            try
            {
                _userRepository.Remove(user.Id);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                var users = _userRepository.GetAll();

                return users.ToList();
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _userRepository.Update(user, user.Id);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }
    }
}
