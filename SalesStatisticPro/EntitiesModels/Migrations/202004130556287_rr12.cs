namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rr12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseOrderInfo", "PurchaseOrder_ID", "dbo.PurchaseOrder");
            DropIndex("dbo.PurchaseOrderInfo", new[] { "PurchaseOrder_ID" });
            DropColumn("dbo.PurchaseOrderInfo", "PurchaseOrder_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderInfo", "PurchaseOrder_ID", c => c.Int());
            CreateIndex("dbo.PurchaseOrderInfo", "PurchaseOrder_ID");
            AddForeignKey("dbo.PurchaseOrderInfo", "PurchaseOrder_ID", "dbo.PurchaseOrder", "ID");
        }
    }
}
