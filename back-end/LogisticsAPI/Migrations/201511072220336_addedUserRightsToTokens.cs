namespace LogisticsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserRightsToTokens : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TokenModels", "UserRights", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TokenModels", "UserRights");
        }
    }
}
