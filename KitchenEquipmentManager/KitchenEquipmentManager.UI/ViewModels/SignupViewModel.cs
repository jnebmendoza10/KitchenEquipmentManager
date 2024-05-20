using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Users;
using KitchenEquipmentManager.UI.Command;

namespace KitchenEquipmentManager.UI.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        private string _selectedUserType;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _userName;
        private string _password;

        private ICommand _signupCommand;

        private readonly IAuthenticationService _authenticationService;

        public SignupViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

            _selectedUserType = UserTypes.FirstOrDefault();
        }

        public ICommand SignUpUserCommand
        {
            get
            {
                if (_signupCommand == null)
                {
                    _signupCommand = new RelayCommand(SignUp);
                }

                return _signupCommand;
            }
        }

        public List<string> UserTypes
        {
            get
            {
                return new List<string>() 
                { 
                    "SuperAdmin",
                    "Admin"
                };
            }
        }
        public string SelectedUserType 
        {
            get => _selectedUserType;
            set => SetProperty(ref _selectedUserType, value, nameof(SelectedUserType));
        }
        public string FirstName 
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value, nameof(FirstName));
        }
        public string LastName 
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value, nameof(LastName));
        }
        public string Email 
        {
            get => _email;
            set => SetProperty(ref _email, value, nameof(Email));
        }
        public string UserName 
        {
            get => _userName;
            set => SetProperty(ref _userName, value, nameof(UserName));
        }
        public string Password 
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(Password));
        }

        public void SignUp()
        {
            if (string.IsNullOrEmpty(FirstName) || 
                string.IsNullOrEmpty(LastName) || 
                string.IsNullOrEmpty(Email) || 
                string.IsNullOrEmpty(UserName) || 
                string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please fill up missing fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            User user = new User();
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.UserType = SelectedUserType;
            user.EmailAddress = Email;
            user.UserName = UserName;
            user.Password = Password;

            bool succesfulRegistration = _authenticationService.Register(user);
            if (succesfulRegistration)
            {
                MessageBox.Show("Registration successful.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Registration unsuccessful.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
            
    }
}
