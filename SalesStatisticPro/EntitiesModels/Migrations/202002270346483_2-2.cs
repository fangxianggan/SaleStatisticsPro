namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrder", "AllPurchaseSettlementAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrder", "PurchaseOrderState", c => c.String(nullable: false, maxLength: 8));
            AddColumn("dbo.PurchaseOrderInfo", "PurchaseSettlementAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderInfo", "PurchaseOrderInfoState", c => c.String(nullable: false, maxLength: 8));
            AddColumn("dbo.SaleOrder", "AllSaleSettlementAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SaleOrder", "SaleOrderState", c => c.String(nullable: false, maxLength: 8));
            AddColumn("dbo.SaleOrderInfo", "SaleSettlementAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SaleOrderInfo", "SaleOrderInfoState", c => c.String(nullable: false, maxLength: 8));
            DropColumn("dbo.PurchaseOrderInfo", "SettlementAmount");
            DropColumn("dbo.PurchaseOrderInfo", "PurchaseOrderState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderInfo", "PurchaseOrderState", c => c.String(nullable: false));
            AddColumn("dbo.PurchaseOrderInfo", "SettlementAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.SaleOrderInfo", "SaleOrderInfoState");
            DropColumn("dbo.SaleOrderInfo", "SaleSettlementAmount");
            DropColumn("dbo.SaleOrder", "SaleOrderState");
            DropColumn("dbo.SaleOrder", "AllSaleSettlementAmount");
            DropColumn("dbo.PurchaseOrderInfo", "PurchaseOrderInfoState");
            DropColumn("dbo.PurchaseOrderInfo", "PurchaseSettlementAmount");
            DropColumn("dbo.PurchaseOrder", "PurchaseOrderState");
            DropColumn("dbo.PurchaseOrder", "AllPurchaseSettlementAmount");
        }
    }
}
