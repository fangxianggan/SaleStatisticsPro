namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123qwe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderInfo", "PurchaseOrder_ID", c => c.Int());
            CreateIndex("dbo.PurchaseOrderInfo", "PurchaseOrder_ID");
            AddForeignKey("dbo.PurchaseOrderInfo", "PurchaseOrder_ID", "dbo.PurchaseOrder", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrderInfo", "PurchaseOrder_ID", "dbo.PurchaseOrder");
            DropIndex("dbo.PurchaseOrderInfo", new[] { "PurchaseOrder_ID" });
            DropColumn("dbo.PurchaseOrderInfo", "PurchaseOrder_ID");
        }
    }
}
