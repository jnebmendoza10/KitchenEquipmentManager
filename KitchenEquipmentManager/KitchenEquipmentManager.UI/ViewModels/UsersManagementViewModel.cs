using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Users;
using KitchenEquipmentManager.UI.Command.Generic;

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

            bool isUpdatedSuccessful = _userService.UpdateUser(user);

            if (isUpdatedSuccessful)
            {
                MessageBox.Show("Update successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Update unsuccessful.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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


            bool isDeleteSuccessful = _userService.DeleteUser(user);

            Users.Remove(user);

            if (isDeleteSuccessful)
            {
                MessageBox.Show("Delete successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Delete successful", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool isCurrentUser(User user)
        {
            return (CurrentUserLoggedIn.UserId == user.Id) && (CurrentUserLoggedIn.UserName == user.UserName);
        }

    }
}
