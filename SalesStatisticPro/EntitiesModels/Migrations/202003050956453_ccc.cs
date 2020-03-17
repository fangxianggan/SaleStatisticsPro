namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ccc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderInfo", "ExpressCompanyCode", c => c.String(maxLength: 64));
            AddColumn("dbo.PurchaseOrderInfo", "ExpressNumber", c => c.String(maxLength: 64));
            AddColumn("dbo.PurchaseOrderInfo", "ExpressFreightAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderInfo", "InternationExpressCompanyCode", c => c.String(maxLength: 64));
            AddColumn("dbo.PurchaseOrderInfo", "DomesticExpressCompanyCode", c => c.String(maxLength: 64));
            AddColumn("dbo.SaleOrderInfo", "ExpressCompanyCode", c => c.String(maxLength: 64));
            AlterColumn("dbo.Category", "CategoryName", c => c.String(nullable: false, maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "CategoryName", c => c.String(nullable: false, maxLength: 64));
            DropColumn("dbo.SaleOrderInfo", "ExpressCompanyCode");
            DropColumn("dbo.PurchaseOrderInfo", "DomesticExpressCompanyCode");
            DropColumn("dbo.PurchaseOrderInfo", "InternationExpressCompanyCode");
            DropColumn("dbo.PurchaseOrderInfo", "ExpressFreightAmount");
            DropColumn("dbo.PurchaseOrderInfo", "ExpressNumber");
            DropColumn("dbo.PurchaseOrderInfo", "ExpressCompanyCode");
        }
    }
}
