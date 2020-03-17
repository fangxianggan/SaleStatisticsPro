namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrder", "BusinessCode", c => c.String(nullable: false, maxLength: 64));
            AddColumn("dbo.PurchaseOrder", "AllInternationFreightAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrder", "AllDomesticFreightAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderInfo", "InternationNumber", c => c.String(maxLength: 64));
            AddColumn("dbo.PurchaseOrderInfo", "DomesticNumber", c => c.String(maxLength: 64));
            AddColumn("dbo.PurchaseOrderInfo", "InternationFreightAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderInfo", "DomesticFreightAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderInfo", "SettlementAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderInfo", "PurchaseOrderState", c => c.String(nullable: false));
            AddColumn("dbo.SaleOrderInfo", "ExpressNumber", c => c.String(maxLength: 64));
            AlterColumn("dbo.Business", "BusinessCode", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Business", "BusinessName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Product", "ProductCode", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Product", "SimpleCode", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Product", "ProductName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.PurchaseOrder", "POrderNum", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.PurchaseOrder", "POrderTitle", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.PurchaseOrder", "USANumber", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.PurchaseOrder", "TransferBinCode", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.PurchaseOrderInfo", "POrderNum", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.PurchaseOrderInfo", "PPOrderNum", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.PurchaseOrderInfo", "PProductCode", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.SaleOrder", "SOrderNum", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.SaleOrder", "PhoneNumber", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.SaleOrderInfo", "SOrderNum", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.SaleOrderInfo", "SSOrderNum", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.SaleOrderInfo", "SProductCode", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.TransferBin", "TransferBinCode", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.TransferBin", "TransferBinName", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.UserInfo", "PhoneNumber", c => c.String(nullable: false, maxLength: 32));
            DropColumn("dbo.Product", "BusinessCode");
            DropColumn("dbo.Product", "BrandCode");
            DropColumn("dbo.Product", "CategoryCode");
            DropColumn("dbo.Product", "ProductTypeName");
            DropColumn("dbo.Product", "SpecsCode");
            DropColumn("dbo.Product", "ProductColor");
            DropColumn("dbo.Product", "UnitCode");
            DropColumn("dbo.PurchaseOrder", "AllFreightAmount");
            DropColumn("dbo.PurchaseOrderInfo", "TrackingNumber");
            DropColumn("dbo.PurchaseOrderInfo", "FreightAmount");
            DropTable("dbo.Brand");
            DropTable("dbo.Category");
            DropTable("dbo.Specs");
            DropTable("dbo.Unit");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UnitCode = c.String(maxLength: 64),
                        UnitName = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUserCode = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUserCode = c.String(maxLength: 32),
                        Remark = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Specs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SpecsCode = c.String(maxLength: 64),
                        SpecsName = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUserCode = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUserCode = c.String(maxLength: 32),
                        Remark = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryCode = c.String(maxLength: 64),
                        CategoryName = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUserCode = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUserCode = c.String(maxLength: 32),
                        Remark = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BrandCode = c.String(maxLength: 64),
                        BrandName = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUserCode = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUserCode = c.String(maxLength: 32),
                        Remark = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PurchaseOrderInfo", "FreightAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderInfo", "TrackingNumber", c => c.String(maxLength: 64));
            AddColumn("dbo.PurchaseOrder", "AllFreightAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Product", "UnitCode", c => c.String(maxLength: 64));
            AddColumn("dbo.Product", "ProductColor", c => c.String(maxLength: 50));
            AddColumn("dbo.Product", "SpecsCode", c => c.String(maxLength: 64));
            AddColumn("dbo.Product", "ProductTypeName", c => c.String(maxLength: 50));
            AddColumn("dbo.Product", "CategoryCode", c => c.String(maxLength: 64));
            AddColumn("dbo.Product", "BrandCode", c => c.String(maxLength: 64));
            AddColumn("dbo.Product", "BusinessCode", c => c.String(maxLength: 64));
            AlterColumn("dbo.UserInfo", "PhoneNumber", c => c.String(maxLength: 32));
            AlterColumn("dbo.TransferBin", "TransferBinName", c => c.String(maxLength: 64));
            AlterColumn("dbo.TransferBin", "TransferBinCode", c => c.String(maxLength: 64));
            AlterColumn("dbo.SaleOrderInfo", "SProductCode", c => c.String(maxLength: 64));
            AlterColumn("dbo.SaleOrderInfo", "SSOrderNum", c => c.String(maxLength: 64));
            AlterColumn("dbo.SaleOrderInfo", "SOrderNum", c => c.String(maxLength: 64));
            AlterColumn("dbo.SaleOrder", "PhoneNumber", c => c.String(maxLength: 32));
            AlterColumn("dbo.SaleOrder", "SOrderNum", c => c.String(maxLength: 64));
            AlterColumn("dbo.PurchaseOrderInfo", "PProductCode", c => c.String(maxLength: 64));
            AlterColumn("dbo.PurchaseOrderInfo", "PPOrderNum", c => c.String(maxLength: 64));
            AlterColumn("dbo.PurchaseOrderInfo", "POrderNum", c => c.String(maxLength: 64));
            AlterColumn("dbo.PurchaseOrder", "TransferBinCode", c => c.String(maxLength: 64));
            AlterColumn("dbo.PurchaseOrder", "USANumber", c => c.String(maxLength: 32));
            AlterColumn("dbo.PurchaseOrder", "POrderTitle", c => c.String(maxLength: 100));
            AlterColumn("dbo.PurchaseOrder", "POrderNum", c => c.String(maxLength: 64));
            AlterColumn("dbo.Product", "ProductName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Product", "SimpleCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.Product", "ProductCode", c => c.String(maxLength: 64));
            AlterColumn("dbo.Business", "BusinessName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Business", "BusinessCode", c => c.String(maxLength: 64));
            DropColumn("dbo.SaleOrderInfo", "ExpressNumber");
            DropColumn("dbo.PurchaseOrderInfo", "PurchaseOrderState");
            DropColumn("dbo.PurchaseOrderInfo", "SettlementAmount");
            DropColumn("dbo.PurchaseOrderInfo", "DomesticFreightAmount");
            DropColumn("dbo.PurchaseOrderInfo", "InternationFreightAmount");
            DropColumn("dbo.PurchaseOrderInfo", "DomesticNumber");
            DropColumn("dbo.PurchaseOrderInfo", "InternationNumber");
            DropColumn("dbo.PurchaseOrder", "AllDomesticFreightAmount");
            DropColumn("dbo.PurchaseOrder", "AllInternationFreightAmount");
            DropColumn("dbo.PurchaseOrder", "BusinessCode");
        }
    }
}
