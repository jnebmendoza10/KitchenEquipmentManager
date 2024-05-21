using System.Windows;
using KitchenEquipmentManager.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenEquipmentManager.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginViewModel _loginViewModel;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private LoginViewModel LoginViewModel
        {
            get
            {
                if (_loginViewModel == null)
                {
                    _loginViewModel = DataContext as LoginViewModel;
                }

                return _loginViewModel;
            }
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            var signupWindow = new SignUpWindow();
            signupWindow.DataContext = LoginViewModel.GetRequiredService().GetService<SignupViewModel>();
            signupWindow.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
