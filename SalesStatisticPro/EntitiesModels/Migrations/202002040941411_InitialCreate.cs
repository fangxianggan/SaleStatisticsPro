namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaleOrder", "UserPurchaseGender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SaleOrder", "UserPurchaseGender");
        }
    }
}
