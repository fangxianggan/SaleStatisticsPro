namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _225535 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ExpressCompany", "MerchantId");
            DropColumn("dbo.Menu", "MerchantId");
            DropColumn("dbo.MerchantRole", "MerchantId");
            DropColumn("dbo.Role", "MerchantId");
            DropColumn("dbo.RoleMenu", "MerchantId");
            DropColumn("dbo.SqlLog", "MerchantId");
            DropColumn("dbo.SysConfig", "MerchantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SysConfig", "MerchantId", c => c.Guid());
            AddColumn("dbo.SqlLog", "MerchantId", c => c.Guid());
            AddColumn("dbo.RoleMenu", "MerchantId", c => c.Guid());
            AddColumn("dbo.Role", "MerchantId", c => c.Guid());
            AddColumn("dbo.MerchantRole", "MerchantId", c => c.Guid());
            AddColumn("dbo.Menu", "MerchantId", c => c.Guid());
            AddColumn("dbo.ExpressCompany", "MerchantId", c => c.Guid());
        }
    }
}
