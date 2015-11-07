namespace LogisticsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedOlIdField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "Id");
            DropColumn("dbo.Items", "Id");
            DropColumn("dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "Id", c => c.Long(nullable: false));
            AddColumn("dbo.Items", "Id", c => c.Long(nullable: false));
            AddColumn("dbo.Categories", "Id", c => c.Long(nullable: false));
        }
    }
}
