namespace LogisticsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        EntityId = c.Guid(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.EntityId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        EntityId = c.Guid(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        LocationId = c.Guid(nullable: false),
                        Quantity = c.Double(nullable: false),
                        MinQuantity = c.Double(nullable: false),
                        Relevance = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Picture = c.String(unicode: false),
                        Status = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.EntityId)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        EntityId = c.Guid(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.EntityId);
            
            CreateTable(
                "dbo.Warnings",
                c => new
                    {
                        ItemEntityId = c.Guid(nullable: false),
                        Message = c.String(unicode: false),
                        EntityId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ItemEntityId)
                .ForeignKey("dbo.Items", t => t.ItemEntityId)
                .Index(t => t.ItemEntityId);
            
            CreateTable(
                "dbo.TokenModels",
                c => new
                    {
                        EntityId = c.Guid(nullable: false, identity: true),
                        Username = c.String(unicode: false),
                        Token = c.String(unicode: false),
                        Roles = c.String(unicode: false),
                        ExpirationDateTime = c.DateTime(nullable: false, precision: 0),
                        Password = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.EntityId);
            
            CreateTable(
                "dbo.ItemsCategoryBinding",
                c => new
                    {
                        ItemEntityId = c.Guid(nullable: false),
                        CategoryEntityId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemEntityId, t.CategoryEntityId })
                .ForeignKey("dbo.Items", t => t.ItemEntityId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryEntityId, cascadeDelete: true)
                .Index(t => t.ItemEntityId)
                .Index(t => t.CategoryEntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Warnings", "ItemEntityId", "dbo.Items");
            DropForeignKey("dbo.Items", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.ItemsCategoryBinding", "CategoryEntityId", "dbo.Categories");
            DropForeignKey("dbo.ItemsCategoryBinding", "ItemEntityId", "dbo.Items");
            DropIndex("dbo.ItemsCategoryBinding", new[] { "CategoryEntityId" });
            DropIndex("dbo.ItemsCategoryBinding", new[] { "ItemEntityId" });
            DropIndex("dbo.Warnings", new[] { "ItemEntityId" });
            DropIndex("dbo.Items", new[] { "LocationId" });
            DropTable("dbo.ItemsCategoryBinding");
            DropTable("dbo.TokenModels");
            DropTable("dbo.Warnings");
            DropTable("dbo.Locations");
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
        }
    }
}
