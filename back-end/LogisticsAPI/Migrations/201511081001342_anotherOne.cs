namespace LogisticsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anotherOne : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TokenModels", "_role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TokenModels", "_role", c => c.Int(nullable: false));
        }
    }
}
