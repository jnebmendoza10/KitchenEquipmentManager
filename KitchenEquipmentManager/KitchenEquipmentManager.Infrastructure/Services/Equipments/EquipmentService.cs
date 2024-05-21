using System;
using System.Collections.Generic;
using System.Linq;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Repository.Services;

namespace KitchenEquipmentManager.Infrastructure.Services.Equipments
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IDataRepository<Equipment> _equipmentRepository;
        private readonly IDataRepository<RegisteredEquipment> _registeredEquipmentRepository;
        public EquipmentService(
            IDataRepository<Equipment> equipmentRepository,
            IDataRepository<RegisteredEquipment> registeredEquipmentRepository)
        {
            _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));
            _registeredEquipmentRepository = registeredEquipmentRepository ?? throw new ArgumentNullException(nameof(registeredEquipmentRepository));
        }
        public void AddEquipment(Equipment equipment)
        {
            try
            {
                _equipmentRepository.Add(equipment);

            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public void RegisterEquipmentToSite(RegisteredEquipment registerEquipment)
        {
            try
            {
                _registeredEquipmentRepository.Add(registerEquipment);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }


        public void DeleteEquipment(Equipment site)
        {
            try
            {
                _equipmentRepository.Remove(site.Id);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public List<Equipment> RetrieveEquipmentsForUser(User user)
        {
            try
            {
                var equipments = _equipmentRepository.GetAll().Where(x => x.UserId == user.Id).ToList();

                return equipments;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            
        }

        public void UpdateEquipment(Equipment site)
        {
            try
            {
                _equipmentRepository.Update(site, site.Id);

            
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }
    }
}
