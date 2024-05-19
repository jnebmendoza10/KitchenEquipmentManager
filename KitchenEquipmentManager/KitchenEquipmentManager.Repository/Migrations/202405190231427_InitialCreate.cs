namespace KitchenEquipmentManager.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipment",
                c => new
                    {
                        equipment_id = c.Guid(nullable: false, identity: true),
                        serial_number = c.String(nullable: false, maxLength: 8),
                        description = c.String(nullable: false, maxLength: 250),
                        condition = c.String(nullable: false, maxLength: 20),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.equipment_id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RegisteredEquipment",
                c => new
                    {
                        id = c.Guid(nullable: false, identity: true),
                        EquipmentId = c.Guid(nullable: false),
                        SiteId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Equipment", t => t.EquipmentId)
                .ForeignKey("dbo.Site", t => t.SiteId, cascadeDelete: true)
                .Index(t => t.EquipmentId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Site",
                c => new
                    {
                        site_id = c.Guid(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        description = c.String(nullable: false, maxLength: 250),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.site_id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        user_id = c.Guid(nullable: false, identity: true),
                        user_type = c.String(nullable: false, maxLength: 50),
                        first_name = c.String(nullable: false, maxLength: 100),
                        last_name = c.String(nullable: false, maxLength: 100),
                        email_address = c.String(nullable: false, maxLength: 50),
                        user_name = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipment", "UserId", "dbo.User");
            DropForeignKey("dbo.RegisteredEquipment", "SiteId", "dbo.Site");
            DropForeignKey("dbo.Site", "UserId", "dbo.User");
            DropForeignKey("dbo.RegisteredEquipment", "EquipmentId", "dbo.Equipment");
            DropIndex("dbo.Site", new[] { "UserId" });
            DropIndex("dbo.RegisteredEquipment", new[] { "SiteId" });
            DropIndex("dbo.RegisteredEquipment", new[] { "EquipmentId" });
            DropIndex("dbo.Equipment", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Site");
            DropTable("dbo.RegisteredEquipment");
            DropTable("dbo.Equipment");
        }
    }
}
