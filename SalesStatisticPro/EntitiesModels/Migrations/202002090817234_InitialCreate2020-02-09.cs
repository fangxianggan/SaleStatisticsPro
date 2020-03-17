namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate20200209 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Prodoct", newName: "Product");
            AddColumn("dbo.Product", "ProductName", c => c.String(maxLength: 50));
            AddColumn("dbo.ProductType", "ProductCode", c => c.String(maxLength: 32));
            AddColumn("dbo.ProductType", "ProductTypeName", c => c.String(maxLength: 50));
            DropColumn("dbo.Product", "ProdoctName");
            DropColumn("dbo.ProductType", "ProdoctCode");
            DropColumn("dbo.ProductType", "ProdoctTypeName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductType", "ProdoctTypeName", c => c.String(maxLength: 50));
            AddColumn("dbo.ProductType", "ProdoctCode", c => c.String(maxLength: 32));
            AddColumn("dbo.Product", "ProdoctName", c => c.String(maxLength: 50));
            DropColumn("dbo.ProductType", "ProductTypeName");
            DropColumn("dbo.ProductType", "ProductCode");
            DropColumn("dbo.Product", "ProductName");
            RenameTable(name: "dbo.Product", newName: "Prodoct");
        }
    }
}
