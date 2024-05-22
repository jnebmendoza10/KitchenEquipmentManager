using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Users;
using KitchenEquipmentManager.UI.Command.Generic;
using KitchenEquipmentManager.UI.Views;

namespace KitchenEquipmentManager.UI.ViewModels
{
    public class UsersManagementViewModel : BaseViewModel
    {
        public ObservableCollection<User> _users = new ObservableCollection<User>();
        private readonly IUserService _userService;
        private UserViewModel _currentUserLoggedIn;

        private ICommand _editUser;
        private ICommand _deleteUser;

        public UsersManagementViewModel(IUserService userService) 
        { 
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            Users = new ObservableCollection<User>(retrieveAllUsers());
        }

        public ICommand EditUserCommand
        {
            get
            {
                if (_editUser == null )
                {
                    _editUser = new RelayCommand<User>(u => edit(u));
                }

                return _editUser;
            }
        }

        public ICommand DeleteUserCommand
        {
            get
            {
                if ( _deleteUser == null )
                {
                    _deleteUser = new RelayCommand<User>(u => delete(u));
                }

                return _deleteUser;
            }
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetProperty(ref _users, value, nameof(Users));
        }

        public List<string> UserTypes
        {
            get
            {
                return new List<string>()
                {
                    "Admin",
                    "SuperAdmin"
                };
            }
        }

        public UserViewModel CurrentUserLoggedIn
        {
            get => _currentUserLoggedIn;
            set => SetProperty(ref _currentUserLoggedIn, value, nameof(CurrentUserLoggedIn));
        }

        private List<User> retrieveAllUsers()
        {
            var users = _userService.GetAllUsers();

            return users ?? new List<User>();
        }

        private void edit(User user)
        {
            if (user == null)
                return;

            bool currentUser = isCurrentUser(user);

            if (string.IsNullOrEmpty(user.UserName))
            {
                MessageBox.Show("Please update empty fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _userService.UpdateUser(user);

                MessageBox.Show("Update successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                if (currentUser)
                {
                    MessageBox.Show("Please relogin to relfect the changes in your account", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    closeUserWindow();
                    navigateToLogin();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void delete(User user)
        {
            if (user == null)
                return;

            if (isCurrentUser(user))
            {
                MessageBox.Show("You cannot delete your own account", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _userService.DeleteUser(user);
                Users.Remove(user);

                MessageBox.Show("Delete successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool isCurrentUser(User user)
        {
            return (CurrentUserLoggedIn.UserId == user.Id);
        }

        private void closeUserWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is UsersWindow)
                {
                    window.Close();
                    continue;
                }
            }
        }

        private void navigateToLogin()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is AdminWindow)
                {
                    window.Close();
                    Application.Current.MainWindow.Show();
                    break;
                }
            }
            
        }

    }
}
