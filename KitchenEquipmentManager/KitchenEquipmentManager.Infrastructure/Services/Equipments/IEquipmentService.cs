using System.Collections.Generic;
using KitchenEquipmentManager.Domain.Models;

namespace KitchenEquipmentManager.Infrastructure.Services.Equipments
{
    public interface IEquipmentService
    {
        List<Equipment> RetrieveEquipmentsForUser(User user);
        bool AddEquipment(Equipment site);
        bool UpdateEquipment(Equipment site);
        bool DeleteEquipment(Equipment site);
    }
}
