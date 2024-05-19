namespace KitchenEquipmentManager.Repository
{
    public class KitchenManagerDbContextFactory : IKitchenManagerDbContextFactory
    {
        public KitchenManagerDbContext CreateDbContext()
        {
            return new KitchenManagerDbContext();
        }
    }
}
