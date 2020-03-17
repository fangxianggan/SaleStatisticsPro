namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product", "BusinessName");
            DropColumn("dbo.Product", "BrandName");
            DropColumn("dbo.Product", "CategoryName");
            DropColumn("dbo.Product", "SpecsName");
            DropColumn("dbo.Product", "UnitName");
            DropColumn("dbo.PurchaseOrder", "TransferBinName");
            DropColumn("dbo.PurchaseOrderInfo", "PProductName");
            DropColumn("dbo.PurchaseOrderInfo", "StockCount");
            DropTable("dbo.Storage");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Storage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(maxLength: 64),
                        SimpleCode = c.String(maxLength: 50),
                        ProductName = c.String(maxLength: 50),
                        PurchaseCount = c.Int(nullable: false),
                        PurchaseAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseFreightAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseAllAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleCount = c.Int(nullable: false),
                        SaleAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleFreightAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleAllAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        ProfitAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUserCode = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUserCode = c.String(maxLength: 32),
                        Remark = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PurchaseOrderInfo", "StockCount", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseOrderInfo", "PProductName", c => c.String(maxLength: 50));
            AddColumn("dbo.PurchaseOrder", "TransferBinName", c => c.String(maxLength: 32));
            AddColumn("dbo.Product", "UnitName", c => c.String(maxLength: 50));
            AddColumn("dbo.Product", "SpecsName", c => c.String(maxLength: 50));
            AddColumn("dbo.Product", "CategoryName", c => c.String(maxLength: 50));
            AddColumn("dbo.Product", "BrandName", c => c.String(maxLength: 50));
            AddColumn("dbo.Product", "BusinessName", c => c.String(maxLength: 50));
        }
    }
}
