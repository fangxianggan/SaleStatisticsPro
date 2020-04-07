namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22553568 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Business", "P_MerchantNo", c => c.String());
            AddColumn("dbo.Category", "P_MerchantNo", c => c.String());
            AddColumn("dbo.Product", "P_MerchantNo", c => c.String());
            AddColumn("dbo.PurchaseOrder", "P_MerchantNo", c => c.String());
            AddColumn("dbo.PurchaseOrderInfo", "P_MerchantNo", c => c.String());
            AddColumn("dbo.SaleOrder", "P_MerchantNo", c => c.String());
            AddColumn("dbo.SaleOrderInfo", "P_MerchantNo", c => c.String());
            AddColumn("dbo.TransferBin", "P_MerchantNo", c => c.String());
            AddColumn("dbo.UserInfo", "P_MerchantNo", c => c.String());
            DropColumn("dbo.Business", "MerchantNo");
            DropColumn("dbo.Category", "MerchantNo");
            DropColumn("dbo.Product", "MerchantNo");
            DropColumn("dbo.PurchaseOrder", "MerchantNo");
            DropColumn("dbo.PurchaseOrderInfo", "MerchantNo");
            DropColumn("dbo.SaleOrder", "MerchantNo");
            DropColumn("dbo.SaleOrderInfo", "MerchantNo");
            DropColumn("dbo.TransferBin", "MerchantNo");
            DropColumn("dbo.UserInfo", "MerchantNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfo", "MerchantNo", c => c.String());
            AddColumn("dbo.TransferBin", "MerchantNo", c => c.String());
            AddColumn("dbo.SaleOrderInfo", "MerchantNo", c => c.String());
            AddColumn("dbo.SaleOrder", "MerchantNo", c => c.String());
            AddColumn("dbo.PurchaseOrderInfo", "MerchantNo", c => c.String());
            AddColumn("dbo.PurchaseOrder", "MerchantNo", c => c.String());
            AddColumn("dbo.Product", "MerchantNo", c => c.String());
            AddColumn("dbo.Category", "MerchantNo", c => c.String());
            AddColumn("dbo.Business", "MerchantNo", c => c.String());
            DropColumn("dbo.UserInfo", "P_MerchantNo");
            DropColumn("dbo.TransferBin", "P_MerchantNo");
            DropColumn("dbo.SaleOrderInfo", "P_MerchantNo");
            DropColumn("dbo.SaleOrder", "P_MerchantNo");
            DropColumn("dbo.PurchaseOrderInfo", "P_MerchantNo");
            DropColumn("dbo.PurchaseOrder", "P_MerchantNo");
            DropColumn("dbo.Product", "P_MerchantNo");
            DropColumn("dbo.Category", "P_MerchantNo");
            DropColumn("dbo.Business", "P_MerchantNo");
        }
    }
}
