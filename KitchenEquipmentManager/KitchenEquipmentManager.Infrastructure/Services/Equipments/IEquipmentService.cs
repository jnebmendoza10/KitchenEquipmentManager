using System.Collections.Generic;
using KitchenEquipmentManager.Domain.Models;

namespace KitchenEquipmentManager.Infrastructure.Services.Equipments
{
    public interface IEquipmentService
    {
        List<Equipment> RetrieveEquipmentsForUser(User user);
        void RegisterEquipmentToSite(RegisteredEquipment registerEquipment);
        void AddEquipment(Equipment site);
        void UpdateEquipment(Equipment site);
        void DeleteEquipment(Equipment site);
    }
}
