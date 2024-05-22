using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Equipments;
using KitchenEquipmentManager.UI.Command;
using KitchenEquipmentManager.UI.Views;

namespace KitchenEquipmentManager.UI.ViewModels
{
    public class RegisterEquipmentViewModel : BaseViewModel
    {
        private ObservableCollection<Site> _sites;
        private SiteViewModel _siteViewModel;
        private Site _selectedSite;
        private Equipment _equipment;

        private readonly IEquipmentService _equipmentService;

        private ICommand _registerCommand;

        public RegisterEquipmentViewModel(IEquipmentService equipmentService) 
        { 
            _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
        }

        public ICommand RegisterCommand
        {
            get
            {
                if (_registerCommand == null)
                {
                    _registerCommand = new RelayCommand(register);
                }

                return _registerCommand;
            }
        }

        public ObservableCollection<Site> Sites
        {
            get => _sites;
            set => SetProperty(ref _sites, value, nameof(Sites));
        }

        public Site SelectedSite
        {
            get => _selectedSite;
            set => SetProperty(ref _selectedSite, value, nameof(SelectedSite));
        }

        public Equipment Equipment
        {
            get => _equipment;
            set => SetProperty(ref _equipment, value, nameof(Equipment));
        }

        public SiteViewModel SiteViewModel
        {
            get => _siteViewModel;
            set => SetProperty(ref _siteViewModel, value, nameof(SiteViewModel));
        }

        private void register()
        {
            var registerEquipment = new RegisteredEquipment();
            registerEquipment.SiteId = SelectedSite.Id;
            registerEquipment.EquipmentId = Equipment.Id;

            try
            {
                _equipmentService.RegisterEquipmentToSite(registerEquipment);

                MessageBox.Show("Registration successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                closeRegisterEquipmentWindow();
            }

            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void closeRegisterEquipmentWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is RegisterEquipmentWindow)
                {
                    window.Close();
                    break;
                }
            }
        }

    }
}
