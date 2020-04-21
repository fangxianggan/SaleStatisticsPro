namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Log_OperateLog", "Version", c => c.String(maxLength: 512));
            DropColumn("dbo.Log_OperateLog", "UrlReferrer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Log_OperateLog", "UrlReferrer", c => c.String(maxLength: 512));
            DropColumn("dbo.Log_OperateLog", "Version");
        }
    }
}
