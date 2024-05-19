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
        public EquipmentService(IDataRepository<Equipment> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));
        }
        public bool AddEquipment(Equipment site)
        {
            try
            {
                _equipmentRepository.Add(site);

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteEquipment(Equipment site)
        {
            try
            {
                _equipmentRepository.Remove(site.Id);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Equipment> RetrieveEquipmentsForUser(User user)
        {
            try
            {
                var equipments = _equipmentRepository.GetAll().Where(x => x.UserId == user.Id).ToList();

                return equipments;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }

        public bool UpdateEquipment(Equipment site)
        {
            try
            {
                _equipmentRepository.Update(site, site.Id);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
