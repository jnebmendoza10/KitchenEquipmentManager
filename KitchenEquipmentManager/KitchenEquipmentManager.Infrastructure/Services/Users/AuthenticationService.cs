using System;
using System.Linq;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Repository.Services;

namespace KitchenEquipmentManager.Infrastructure.Services.Users
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordService _passwordService;
        private readonly IDataRepository<User> _userRepository;
        public AuthenticationService(
            IPasswordService passwordService,
            IDataRepository<User> dataRepository) 
        { 
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
            _userRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }
        public User Login(string username, string password)
        {
            try
            {
                // Retrieved user
                var user = _userRepository.GetAll()
                    .Where(a => a.UserName == username).FirstOrDefault();
                if (user == null)
                {
                    // throw Exception indicating that user is not existing.
                    // Display a generic error ( Invalid user name or password.)
                    throw new Exception();
                }

                // Validate password
                if (!_passwordService.ValidatePassword(password, user.Password))
                {
                    // Display a generic error ( Invalid user name or password.)
                    throw new Exception();
                }

                return user;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }

        public bool Register(User user)
        {
            try
            {
                //Check if username already exists.
                var retrievedUser = _userRepository.GetAll()
                    .Where(a => a.UserName == user.UserName).FirstOrDefault();
                if (retrievedUser != null)
                {
                    return false; // username already exists
                }
                _userRepository.Add(user);

                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
