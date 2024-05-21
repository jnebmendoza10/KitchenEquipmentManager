using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Equipments;
using KitchenEquipmentManager.UI.Command;
using KitchenEquipmentManager.UI.Command.Generic;
using KitchenEquipmentManager.UI.Validations;
using KitchenEquipmentManager.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenEquipmentManager.UI.ViewModels
{
    public class EquipmentViewModel : BaseViewModel
    {
        private ObservableCollection<Equipment> _equipments;
        private ObservableCollection<Site> _sites;
        private string _serialNumber;
        private string _description;
        private string _condition;
        private UserViewModel _currentUserLoggedIn = new UserViewModel();

        private readonly IEquipmentService _equipmentService;
        private readonly IServiceProvider _serviceProvider;

        private ICommand _addEquipmentCommand;
        private ICommand _updateEquipmentCommand;
        private ICommand _removeEquipmentCommand;
        private ICommand _cancelCommand;
        private ICommand _registerEquipmentCommand;

        public EquipmentViewModel(
            IEquipmentService equipmentService,
             IServiceProvider serviceProvider) 
        {
            _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public ICommand RegisterEquipmentCommand
        {
            get
            {
                if (_registerEquipmentCommand == null)
                {
                    _registerEquipmentCommand = new RelayCommand<Equipment>(e => registerEquipment(e));
                }

                return _registerEquipmentCommand;
            }
        }

        public ICommand AddEquipmentCommand
        {
            get
            {
                if (_addEquipmentCommand == null)
                {
                    _addEquipmentCommand = new RelayCommand(addEquipment);
                }

                return _addEquipmentCommand;
            }
        }

        public ICommand UpdateEquipmentCommand
        {
            get
            {
                if (_updateEquipmentCommand == null)
                {
                    _updateEquipmentCommand = new RelayCommand<Equipment>(u => updateEquipment(u));
                }

                return _updateEquipmentCommand;
            }
        }

        public ICommand DeleteEquipmentCommand
        {
            get
            {
                if (_removeEquipmentCommand == null)
                {
                    _removeEquipmentCommand = new RelayCommand<Equipment>(d => deleteEquipment(d));
                }

                return _removeEquipmentCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(clearFields);
                }

                return _cancelCommand;
            }
        }

        public ObservableCollection<Equipment> Equipments
        {
            get => _equipments;
            set => SetProperty(ref _equipments, value, nameof(Equipment));
        }
        public ObservableCollection<Site> Sites
        {
            get => _sites;
            set => SetProperty(ref _sites, value, nameof(Sites));
        }


        public string EquipmentSerialNumber
        {
            get => _serialNumber;
            set => SetProperty(ref _serialNumber, value, nameof(EquipmentSerialNumber));
        }

        public string EquipmentDescription
        {
            get => _description;
            set => SetProperty(ref _description, value, nameof(EquipmentDescription));
        }

        public string EquipmentCondition
        {
            get => _condition;
            set => SetProperty(ref _condition, value, nameof(EquipmentCondition));
        }

        public UserViewModel CurrentUserLoggedIn
        {
            get => _currentUserLoggedIn;
            set => SetProperty(ref _currentUserLoggedIn, value, nameof(CurrentUserLoggedIn));
        }

        public void RetrieveEquipments()
        {
            var user = new User();
            user.Id = CurrentUserLoggedIn.UserId;
            user.UserName = CurrentUserLoggedIn.UserName;
            user.UserType = CurrentUserLoggedIn.UserType;

            var equipments = _equipmentService.RetrieveEquipmentsForUser(user);

            Equipments = new ObservableCollection<Equipment>(equipments);
        }

        public List<string> Conditions
        {
            get
            {
                return new List<string>()
                {
                    "Working",
                    "Not Working"
                };
            }
        }

        public IServiceProvider GetRequiredService()
        {
            return _serviceProvider;
        }

        private void addEquipment()
        {
            if (string.IsNullOrEmpty(EquipmentSerialNumber) ||
                string.IsNullOrEmpty(EquipmentDescription) ||
                string.IsNullOrEmpty(EquipmentCondition))
            {
                MessageBox.Show("Please fill up empty fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
            var equipment = new Equipment();
            equipment.SerialNumber = EquipmentSerialNumber;
            equipment.Description = EquipmentDescription;
            equipment.Condition = EquipmentCondition;
            equipment.UserId = CurrentUserLoggedIn.UserId;

            string errorMessages = equipment.IsEquipmentValid();

            if (!string.IsNullOrEmpty(errorMessages))
            {
                MessageBox.Show(errorMessages, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            try
            {
                _equipmentService.AddEquipment(equipment);

                Equipments.Add(equipment);
                MessageBox.Show("Added successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void registerEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;

            var registerWindow = new RegisterEquipmentWindow();

            var registerEquipmentViewModel = GetRequiredService().GetService<RegisterEquipmentViewModel>();

            registerEquipmentViewModel.Sites = Sites;
            registerEquipmentViewModel.Equipment = equipment;

            registerWindow.DataContext = registerEquipmentViewModel;
            registerWindow.ShowDialog();
        }

        private void updateEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;

            if (string.IsNullOrEmpty(EquipmentSerialNumber) ||
                string.IsNullOrEmpty(EquipmentDescription) ||
                string.IsNullOrEmpty(EquipmentCondition))
            {
                MessageBox.Show("Please fill up empty fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (Equipments.Contains(equipment))
            {
                MessageBox.Show("No updates were made.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            string errorMessages = equipment.IsEquipmentValid();

            if (!string.IsNullOrEmpty(errorMessages))
            {
                MessageBox.Show(errorMessages, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            try
            {
                _equipmentService.UpdateEquipment(equipment);

                MessageBox.Show("Update successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;

            try
            {
                _equipmentService.DeleteEquipment(equipment);

                Equipments.Remove(equipment);
                MessageBox.Show("Delete successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void clearFields()
        {
            EquipmentDescription = "";
            EquipmentCondition = "";
            EquipmentSerialNumber = "";
        }

    }
}
