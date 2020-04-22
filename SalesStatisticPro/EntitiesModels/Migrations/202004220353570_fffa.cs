namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fffa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Log_OperateLog", "UrlReferrer", c => c.String(maxLength: 512));
            AlterColumn("dbo.Log_OperateLog", "ControllerName", c => c.String(maxLength: 128));
            AlterColumn("dbo.Log_OperateLog", "ResponseStatus", c => c.String(maxLength: 64));
            DropColumn("dbo.Log_OperateLog", "Version");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Log_OperateLog", "Version", c => c.String(maxLength: 512));
            AlterColumn("dbo.Log_OperateLog", "ResponseStatus", c => c.String(maxLength: 128));
            AlterColumn("dbo.Log_OperateLog", "ControllerName", c => c.String(maxLength: 64));
            DropColumn("dbo.Log_OperateLog", "UrlReferrer");
        }
    }
}
