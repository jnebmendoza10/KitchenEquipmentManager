using System.Threading;
using System.Windows;
using KitchenEquipmentManager.Infrastructure;
using KitchenEquipmentManager.UI.ViewModels;
using KitchenEquipmentManager.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace KitchenEquipmentManager.UI
{
    public class Program
    {
        private static IHost _host;
        public static void Main(string[] args)
        {
            _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register Infrastructure services
                services.AddInfrastructure();

                // Register ViewModels
                services.AddSingleton<UserViewModel>();

                services.AddTransient<LoginViewModel>();
                services.AddTransient<SignupViewModel>();
                services.AddTransient<AdminViewModel>();
                services.AddTransient<UsersManagementViewModel>();
                services.AddTransient<EquipmentViewModel>();
                services.AddTransient<SiteViewModel>();
            })
            .Build();


            var staThread = new Thread(() =>
            {
                var application = new Application();

                // Create and configure the LoginWindow
                var loginWindow = new LoginWindow();
                loginWindow.DataContext = _host.Services.GetService<LoginViewModel>();

                // Set the MainWindow property
                application.MainWindow = loginWindow;

                Application.Current.MainWindow.Show();

                // Start the application
                application.Run();
            });

            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
        }

    }
}
