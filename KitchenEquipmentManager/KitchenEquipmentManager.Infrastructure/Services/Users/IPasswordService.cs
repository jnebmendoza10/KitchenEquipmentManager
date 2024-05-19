namespace KitchenEquipmentManager.Infrastructure.Services.Users
{
    public interface IPasswordService
    {
        string CreateHash(string password);
        bool ValidatePassword(string password, string hashedPassword);
    }
}
