namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SaleOrderInfo", "SaleOrderViewModel_ID", "dbo.SaleOrder");
            DropIndex("dbo.SaleOrderInfo", new[] { "SaleOrderViewModel_ID" });
            DropColumn("dbo.SaleOrder", "Discriminator");
            DropColumn("dbo.SaleOrderInfo", "BusinessName");
            DropColumn("dbo.SaleOrderInfo", "BrandName");
            DropColumn("dbo.SaleOrderInfo", "ProductName");
            DropColumn("dbo.SaleOrderInfo", "CategoryName");
            DropColumn("dbo.SaleOrderInfo", "ProductTypeName");
            DropColumn("dbo.SaleOrderInfo", "SimpleCode");
            DropColumn("dbo.SaleOrderInfo", "SpecsName");
            DropColumn("dbo.SaleOrderInfo", "ProductColor");
            DropColumn("dbo.SaleOrderInfo", "UnitName");
            DropColumn("dbo.SaleOrderInfo", "Discriminator");
            DropColumn("dbo.SaleOrderInfo", "SaleOrderViewModel_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SaleOrderInfo", "SaleOrderViewModel_ID", c => c.Int());
            AddColumn("dbo.SaleOrderInfo", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.SaleOrderInfo", "UnitName", c => c.String(maxLength: 50));
            AddColumn("dbo.SaleOrderInfo", "ProductColor", c => c.String(maxLength: 50));
            AddColumn("dbo.SaleOrderInfo", "SpecsName", c => c.String(maxLength: 50));
            AddColumn("dbo.SaleOrderInfo", "SimpleCode", c => c.String());
            AddColumn("dbo.SaleOrderInfo", "ProductTypeName", c => c.String());
            AddColumn("dbo.SaleOrderInfo", "CategoryName", c => c.String());
            AddColumn("dbo.SaleOrderInfo", "ProductName", c => c.String());
            AddColumn("dbo.SaleOrderInfo", "BrandName", c => c.String());
            AddColumn("dbo.SaleOrderInfo", "BusinessName", c => c.String());
            AddColumn("dbo.SaleOrder", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.SaleOrderInfo", "SaleOrderViewModel_ID");
            AddForeignKey("dbo.SaleOrderInfo", "SaleOrderViewModel_ID", "dbo.SaleOrder", "ID");
        }
    }
}
