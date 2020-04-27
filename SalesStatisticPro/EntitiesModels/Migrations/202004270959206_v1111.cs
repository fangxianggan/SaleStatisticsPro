namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1111 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "SimpleCode", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Product", "ProductName", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "ProductName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Product", "SimpleCode", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
