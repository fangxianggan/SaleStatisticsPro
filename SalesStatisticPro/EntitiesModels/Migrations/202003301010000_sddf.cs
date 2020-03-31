namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sddf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menu", "Path", c => c.String(maxLength: 32));
            AddColumn("dbo.Menu", "Icon", c => c.String(maxLength: 32));
            AddColumn("dbo.Menu", "Hidden", c => c.Boolean(nullable: false));
            DropColumn("dbo.Menu", "MenuUrl");
            DropColumn("dbo.Menu", "MenuIcon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menu", "MenuIcon", c => c.String(maxLength: 32));
            AddColumn("dbo.Menu", "MenuUrl", c => c.String(maxLength: 32));
            DropColumn("dbo.Menu", "Hidden");
            DropColumn("dbo.Menu", "Icon");
            DropColumn("dbo.Menu", "Path");
        }
    }
}
