using System;
using System.Windows;
using KitchenEquipmentManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenEquipmentManager.UI.Views
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private AdminViewModel _adminViewModel;
        private const string ADMIN = "Admin";

        public AdminWindow()
        {
            InitializeComponent();
        }

        private AdminViewModel AdminViewModel
        {
            get
            {
                if (_adminViewModel == null)
                {
                    _adminViewModel = DataContext as AdminViewModel;
                }

                return _adminViewModel;
            }
        }

        private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Add logic to check if the logged in user has access to this one 
            if (AdminViewModel.UserViewModel.UserType == ADMIN)
            {
                MessageBox.Show("You do not have permission to access this menu", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var usersMenuWindow = new UsersWindow();

            var userManagementViewModel = AdminViewModel.GetRequiredService().GetService<UsersManagementViewModel>();
            userManagementViewModel.CurrentUserLoggedIn = AdminViewModel.UserViewModel;
            usersMenuWindow.DataContext = userManagementViewModel;
            usersMenuWindow.ShowDialog();
        }

        private void SitesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var sitesMenuWindow = new SiteWindow();
            var sitesMenuViewModel = AdminViewModel.GetRequiredService().GetService<SiteViewModel>();
            sitesMenuViewModel.CurrentUserLoggedIn = AdminViewModel.UserViewModel;
            sitesMenuViewModel.GetSites();

            sitesMenuWindow.DataContext = sitesMenuViewModel;
            sitesMenuWindow.ShowDialog();
        }

        private void EquipmentsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var equipmentMenuWindow = new EquipmentWindow();

            //Retrieve sites to that user can register it.
            var sitesMenuViewModel = AdminViewModel.GetRequiredService().GetService<SiteViewModel>();
            sitesMenuViewModel.CurrentUserLoggedIn = AdminViewModel.UserViewModel;
            sitesMenuViewModel.GetSites();

            var equipmentMenuViewModel = AdminViewModel.GetRequiredService().GetService<EquipmentViewModel>();
            equipmentMenuViewModel.CurrentUserLoggedIn = AdminViewModel.UserViewModel;
            equipmentMenuViewModel.RetrieveEquipments();


            equipmentMenuViewModel.Sites = sitesMenuViewModel.Sites;
            
            equipmentMenuWindow.DataContext = equipmentMenuViewModel;
            
            equipmentMenuWindow.ShowDialog();
        }

        private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Create a logic to dispose all views and viewmodels.
            // Redirect the user to Login Window
            this.Close();
            Application.Current.MainWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
