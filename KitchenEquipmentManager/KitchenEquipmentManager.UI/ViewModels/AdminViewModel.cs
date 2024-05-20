using System;

namespace KitchenEquipmentManager.UI.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private UserViewModel _userViewModel = new UserViewModel();

        public AdminViewModel(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public UserViewModel UserViewModel
        {
            get => _userViewModel;
            set => SetProperty(ref _userViewModel, value, nameof(UserViewModel));
        }

        public IServiceProvider GetRequiredService()
        {
            return _serviceProvider;
        }
    }
}
