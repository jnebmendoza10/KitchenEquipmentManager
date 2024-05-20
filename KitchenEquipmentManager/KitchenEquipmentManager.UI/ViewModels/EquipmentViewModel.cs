using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Equipments;
using KitchenEquipmentManager.UI.Command;
using KitchenEquipmentManager.UI.Command.Generic;

namespace KitchenEquipmentManager.UI.ViewModels
{
    public class EquipmentViewModel : BaseViewModel
    {
        private ObservableCollection<Equipment> _equipments;
        private string _serialNumber;
        private string _description;
        private string _condition;
        private UserViewModel _currentUserLoggedIn = new UserViewModel();

        private readonly IEquipmentService _equipmentService;

        private ICommand _addEquipmentCommand;
        private ICommand _updateEquipmentCommand;
        private ICommand _removeEquipmentCommand;
        private ICommand _cancelCommand;

        public EquipmentViewModel(IEquipmentService equipmentService) 
        {
            _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
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

            bool isAdded = _equipmentService.AddEquipment(equipment);

            if (isAdded)
            {
                Equipments.Add(equipment);
                MessageBox.Show("Added successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("There is an error adding the item.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void updateEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;

            bool isUpdateSuccessful = _equipmentService.UpdateEquipment(equipment);

            if (isUpdateSuccessful)
            {
                MessageBox.Show("Update successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Update unsuccessful.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;

            bool isDeleteSuccessful = _equipmentService.DeleteEquipment(equipment);

            if (isDeleteSuccessful)
            {
                Equipments.Remove(equipment);
                MessageBox.Show("Delete successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Delete successful", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
