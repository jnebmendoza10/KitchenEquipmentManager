using System;

namespace KitchenEquipmentManager.Infrastructure.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException()
            : base("User name already exists.")
        {
        }
    }
}
