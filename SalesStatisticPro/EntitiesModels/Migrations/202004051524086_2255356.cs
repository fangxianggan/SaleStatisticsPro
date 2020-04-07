namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2255356 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Business", "MerchantNo", c => c.String());
            AddColumn("dbo.Category", "MerchantNo", c => c.String());
            AddColumn("dbo.Product", "MerchantNo", c => c.String());
            AddColumn("dbo.PurchaseOrder", "MerchantNo", c => c.String());
            AddColumn("dbo.PurchaseOrderInfo", "MerchantNo", c => c.String());
            AddColumn("dbo.SaleOrder", "MerchantNo", c => c.String());
            AddColumn("dbo.SaleOrderInfo", "MerchantNo", c => c.String());
            AddColumn("dbo.TransferBin", "MerchantNo", c => c.String());
            AddColumn("dbo.UserInfo", "MerchantNo", c => c.String());
            DropColumn("dbo.Business", "MerchantId");
            DropColumn("dbo.Category", "MerchantId");
            DropColumn("dbo.Product", "MerchantId");
            DropColumn("dbo.PurchaseOrder", "MerchantId");
            DropColumn("dbo.PurchaseOrderInfo", "MerchantId");
            DropColumn("dbo.SaleOrder", "MerchantId");
            DropColumn("dbo.SaleOrderInfo", "MerchantId");
            DropColumn("dbo.TransferBin", "MerchantId");
            DropColumn("dbo.UserInfo", "MerchantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfo", "MerchantId", c => c.Guid());
            AddColumn("dbo.TransferBin", "MerchantId", c => c.Guid());
            AddColumn("dbo.SaleOrderInfo", "MerchantId", c => c.Guid());
            AddColumn("dbo.SaleOrder", "MerchantId", c => c.Guid());
            AddColumn("dbo.PurchaseOrderInfo", "MerchantId", c => c.Guid());
            AddColumn("dbo.PurchaseOrder", "MerchantId", c => c.Guid());
            AddColumn("dbo.Product", "MerchantId", c => c.Guid());
            AddColumn("dbo.Category", "MerchantId", c => c.Guid());
            AddColumn("dbo.Business", "MerchantId", c => c.Guid());
            DropColumn("dbo.UserInfo", "MerchantNo");
            DropColumn("dbo.TransferBin", "MerchantNo");
            DropColumn("dbo.SaleOrderInfo", "MerchantNo");
            DropColumn("dbo.SaleOrder", "MerchantNo");
            DropColumn("dbo.PurchaseOrderInfo", "MerchantNo");
            DropColumn("dbo.PurchaseOrder", "MerchantNo");
            DropColumn("dbo.Product", "MerchantNo");
            DropColumn("dbo.Category", "MerchantNo");
            DropColumn("dbo.Business", "MerchantNo");
        }
    }
}
