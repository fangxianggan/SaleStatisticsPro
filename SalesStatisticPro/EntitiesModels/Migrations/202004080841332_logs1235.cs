namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logs1235 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Business", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.Category", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.ExpressCompany", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.Menu", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.MerchantRole", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.Product", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.PurchaseOrder", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.PurchaseOrderInfo", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.Role", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.RoleMenu", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.SaleOrder", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.SaleOrderInfo", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.SysConfig", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.TransferBin", "UpdateTime", c => c.DateTime());
            AlterColumn("dbo.UserInfo", "UpdateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfo", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TransferBin", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SysConfig", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SaleOrderInfo", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SaleOrder", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RoleMenu", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Role", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseOrderInfo", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseOrder", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Product", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MerchantRole", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Menu", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExpressCompany", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Category", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Business", "UpdateTime", c => c.DateTime(nullable: false));
        }
    }
}
