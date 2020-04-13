namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rr126 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseOrder", "USANumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrder", "USANumber", c => c.String(maxLength: 32));
        }
    }
}
