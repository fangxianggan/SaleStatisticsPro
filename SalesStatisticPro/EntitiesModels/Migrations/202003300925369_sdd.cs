namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoleMenu", "RoleCode", c => c.String());
            DropColumn("dbo.RoleMenu", "RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoleMenu", "RoleId", c => c.Int(nullable: false));
            DropColumn("dbo.RoleMenu", "RoleCode");
        }
    }
}
