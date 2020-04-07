namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22553 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menu", "Sort", c => c.Int(nullable: false));
            AddColumn("dbo.Menu", "NoChildren", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menu", "NoChildren");
            DropColumn("dbo.Menu", "Sort");
        }
    }
}
