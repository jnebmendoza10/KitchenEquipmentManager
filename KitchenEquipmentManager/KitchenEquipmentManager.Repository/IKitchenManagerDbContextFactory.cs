namespace KitchenEquipmentManager.Repository
{
    public interface IKitchenManagerDbContextFactory
    {
        KitchenManagerDbContext CreateDbContext();
    }
}
