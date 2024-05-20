using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using KitchenEquipmentManager.Domain.Models;

namespace KitchenEquipmentManager.Repository
{
    public class KitchenManagerDbContext : DbContext
    {
        private const string CONNECTION_STRING = "Data Source=DESKTOP-9K62KT2;Initial Catalog=KitchenManagerDB;Integrated Security=True";
        public KitchenManagerDbContext() : base(CONNECTION_STRING)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<RegisteredEquipment> RegisteredEquipments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>()
                .ToTable("User")
                .HasKey(u => u.Id)
                .Property(u => u.Id).HasColumnName("user_id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>()
                .Property(u => u.UserType).HasColumnName("user_type").IsRequired().HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName).HasColumnName("first_name").IsRequired().HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.LastName).HasColumnName("last_name").IsRequired().HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.EmailAddress).HasColumnName("email_address").IsRequired().HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.UserName).HasColumnName("user_name").IsRequired().HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Password).HasColumnName("password").IsRequired().HasMaxLength(100);

            #endregion User

            #region Site

            modelBuilder.Entity<Site>()
                .ToTable("Site")
                .HasKey(s => s.Id)
                .Property(s => s.Id).HasColumnName("site_id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Site>()
                .Property(s => s.Description).HasColumnName("description").IsRequired().HasMaxLength(250);

            modelBuilder.Entity<Site>()
                .Property(s => s.Active).HasColumnName("active");

            modelBuilder.Entity<Site>()
                .HasRequired(s => s.User)
                .WithMany(u => u.Sites)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false);

            #endregion Site

            #region Equipment

            modelBuilder.Entity<Equipment>()
                .ToTable("Equipment")
                .HasKey(e => e.Id)
                .Property(e => e.Id).HasColumnName("equipment_id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.SerialNumber).HasColumnName("serial_number").IsRequired().HasMaxLength(8);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Description).HasColumnName("description").IsRequired().HasMaxLength(250);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Condition).HasColumnName("condition").IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Equipment>()
                .HasRequired(e => e.User)
                .WithMany(u => u.Equipment)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            #endregion Equipment

            #region Registered Equipment

            modelBuilder.Entity<RegisteredEquipment>()
                .ToTable("RegisteredEquipment")
                .HasKey(re => re.Id)
                .Property(re => re.Id).HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<RegisteredEquipment>()
                .HasRequired(re => re.Equipment)
                .WithMany(e => e.RegisteredEquipment)
                .HasForeignKey(re => re.EquipmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RegisteredEquipment>()
                .HasRequired(re => re.Site)
                .WithMany(s => s.RegisteredEquipment)
                .HasForeignKey(re => re.SiteId)
                .WillCascadeOnDelete(true); //Enable cascading delete for RegisteredEquipment when a Site is deleted

            #endregion Registered Equipment

            base.OnModelCreating(modelBuilder);
        }
    }
}
