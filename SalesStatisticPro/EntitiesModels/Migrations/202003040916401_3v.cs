namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3v : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "CategoryCode", c => c.String(maxLength: 64));
            AlterColumn("dbo.PurchaseOrder", "USANumber", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseOrder", "USANumber", c => c.String(nullable: false, maxLength: 32));
            DropColumn("dbo.Product", "CategoryCode");
        }
    }
}
