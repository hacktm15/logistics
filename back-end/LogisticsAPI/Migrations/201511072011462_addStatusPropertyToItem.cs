namespace LogisticsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatusPropertyToItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Status", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Status");
        }
    }
}
