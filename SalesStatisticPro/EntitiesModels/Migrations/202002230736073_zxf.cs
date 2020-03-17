namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zxf : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseOrderInfo", "ProductCode");
            DropColumn("dbo.PurchaseOrderInfo", "ProductName");
            DropColumn("dbo.PurchaseOrderInfo", "SimpleCode");
            DropColumn("dbo.PurchaseOrderInfo", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderInfo", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.PurchaseOrderInfo", "SimpleCode", c => c.String());
            AddColumn("dbo.PurchaseOrderInfo", "ProductName", c => c.String());
            AddColumn("dbo.PurchaseOrderInfo", "ProductCode", c => c.String());
        }
    }
}
