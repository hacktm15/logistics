namespace LogisticsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingRoleProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TokenModels", "Roles", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TokenModels", "Roles");
        }
    }
}
