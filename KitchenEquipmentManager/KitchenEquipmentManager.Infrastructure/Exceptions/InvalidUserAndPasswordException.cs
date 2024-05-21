using System;

namespace KitchenEquipmentManager.Infrastructure.Exceptions
{
    public class InvalidUserAndPasswordException : Exception
    {
        public InvalidUserAndPasswordException()
            : base("Invalid username or password.")
        {
        }
    }
}
