namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logs1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SqlLog", newName: "Log_SqlLog");
            DropPrimaryKey("dbo.Log_SqlLog");
            AddColumn("dbo.Log_SqlLog", "CreateUserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Log_SqlLog", "CreateUserName", c => c.String());
            AlterColumn("dbo.Log_SqlLog", "OperateSql", c => c.String());
            AlterColumn("dbo.Log_SqlLog", "Parameter", c => c.String());
            AlterColumn("dbo.Log_SqlLog", "CreateUserCode", c => c.String());
            AddPrimaryKey("dbo.Log_SqlLog", "SqlLogId");
            DropColumn("dbo.Log_SqlLog", "ID");
            DropColumn("dbo.Log_SqlLog", "UpdateTime");
            DropColumn("dbo.Log_SqlLog", "UpdateUserCode");
            DropColumn("dbo.Log_SqlLog", "Remark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Log_SqlLog", "Remark", c => c.String(maxLength: 200));
            AddColumn("dbo.Log_SqlLog", "UpdateUserCode", c => c.String(maxLength: 32));
            AddColumn("dbo.Log_SqlLog", "UpdateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Log_SqlLog", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Log_SqlLog");
            AlterColumn("dbo.Log_SqlLog", "CreateUserCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.Log_SqlLog", "Parameter", c => c.String(maxLength: 500));
            AlterColumn("dbo.Log_SqlLog", "OperateSql", c => c.String(maxLength: 2000));
            DropColumn("dbo.Log_SqlLog", "CreateUserName");
            DropColumn("dbo.Log_SqlLog", "CreateUserId");
            AddPrimaryKey("dbo.Log_SqlLog", "ID");
            RenameTable(name: "dbo.Log_SqlLog", newName: "SqlLog");
        }
    }
}
