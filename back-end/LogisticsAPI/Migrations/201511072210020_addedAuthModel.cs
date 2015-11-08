namespace LogisticsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAuthModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TokenModels",
                c => new
                    {
                        EntityId = c.Guid(nullable: false, identity: true),
                        Username = c.String(unicode: false),
                        Token = c.String(unicode: false),
                        ExpirationDateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.EntityId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TokenModels");
        }
    }
}
