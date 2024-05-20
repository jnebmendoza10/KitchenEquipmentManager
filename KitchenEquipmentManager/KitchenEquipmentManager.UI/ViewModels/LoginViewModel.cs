using System;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManager.Infrastructure.Services.Users;
using KitchenEquipmentManager.UI.Command;
using KitchenEquipmentManager.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenEquipmentManager.UI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private ICommand _loginCommand;

        private readonly IAuthenticationService _authenticationService;
        private readonly IServiceProvider _serviceProvider;

        public LoginViewModel(
            IAuthenticationService authenticationService,
            IServiceProvider serviceProvider)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public ICommand LoginUserCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(Login);
                }

                return _loginCommand;
            }
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value, nameof(Username));
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(Password));
        }

        public void Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var user = _authenticationService.Login(Username, Password);

            if (user != null)
            {
                Application.Current.MainWindow.Hide();
                var adminViewModel = GetRequiredService().GetService<AdminViewModel>();
                adminViewModel.UserViewModel.UserId = user.Id;
                adminViewModel.UserViewModel.UserType = user.UserType;
                adminViewModel.UserViewModel.UserName = user.UserName;

                AdminWindow adminWindow = new AdminWindow();
                adminWindow.DataContext = adminViewModel;
                adminWindow.ShowDialog();

                return;
            }

            MessageBox.Show("Login unsuccessful", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public IServiceProvider GetRequiredService()
        {
            return _serviceProvider;
        }
    }
}
