using System;
using System.Linq;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Exceptions;
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
                var users = _userRepository.GetAll().ToList();

                var retrivedUser = users.Find(x => x.UserName.Equals(username));

                if (retrivedUser == null)
                {
                    // throw Exception indicating that user is not existing.
                    // Display a generic error ( Invalid user name or password.)
                    throw new InvalidUserAndPasswordException();
                }

                // Validate password
                if (!_passwordService.ValidatePassword(password, retrivedUser.Password))
                {
                    // Display a generic error ( Invalid user name or password.)
                    throw new InvalidUserAndPasswordException();
                }

                return retrivedUser;
            }
            catch (InvalidUserAndPasswordException ex)
            {
                throw ex;
            }
            
        }

        public void Register(User user)
        {
            try
            {
                //Check if username already exists.
                var retrievedUser = _userRepository.GetAll()
                    .Where(a => a.UserName == user.UserName).FirstOrDefault();
                if (retrievedUser != null)
                {
                    throw new UserAlreadyExistsException();
                }
                user.Password = _passwordService.CreateHash(user.Password);
                _userRepository.Add(user);
            }
            catch (UserAlreadyExistsException ex)
            {
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }
    }
}
