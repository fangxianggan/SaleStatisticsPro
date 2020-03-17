namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaleOrder", "PhoneNumber", c => c.String(maxLength: 32));
            DropColumn("dbo.SaleOrder", "UserPurchase");
            DropColumn("dbo.SaleOrder", "UserPurchasePhone");
            DropColumn("dbo.SaleOrder", "UserPurchaseGender");
            DropColumn("dbo.SaleOrder", "UserPurchaseAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SaleOrder", "UserPurchaseAddress", c => c.String(maxLength: 100));
            AddColumn("dbo.SaleOrder", "UserPurchaseGender", c => c.Int(nullable: false));
            AddColumn("dbo.SaleOrder", "UserPurchasePhone", c => c.String(maxLength: 64));
            AddColumn("dbo.SaleOrder", "UserPurchase", c => c.String(maxLength: 64));
            DropColumn("dbo.SaleOrder", "PhoneNumber");
        }
    }
}
