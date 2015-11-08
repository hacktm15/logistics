namespace LogisticsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPasswordToTokenModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TokenModels", "_role", c => c.Int(nullable: false));
            AddColumn("dbo.TokenModels", "Password", c => c.String(unicode: false));
            DropColumn("dbo.TokenModels", "UserRights");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TokenModels", "UserRights", c => c.Int(nullable: false));
            DropColumn("dbo.TokenModels", "Password");
            DropColumn("dbo.TokenModels", "_role");
        }
    }
}
