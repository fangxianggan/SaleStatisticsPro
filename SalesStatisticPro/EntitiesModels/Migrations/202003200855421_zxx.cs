namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zxx : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Log_DataLog",
                c => new
                    {
                        DataLogId = c.Guid(nullable: false),
                        OperateType = c.String(maxLength: 16),
                        OperateTable = c.String(maxLength: 64),
                        OperationBefore = c.String(),
                        OperationAfterData = c.String(),
                        CreateUserId = c.Guid(nullable: false),
                        CreateUserCode = c.String(),
                        CreateUserName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DataLogId);
            
            CreateTable(
                "dbo.Log_ExceptionLog",
                c => new
                    {
                        ExceptionLogId = c.Guid(nullable: false),
                        Message = c.String(),
                        StackTrace = c.String(),
                        InnerException = c.String(),
                        ExceptionType = c.String(maxLength: 128),
                        ServerHost = c.String(maxLength: 128),
                        ClientHost = c.String(maxLength: 128),
                        Runtime = c.String(maxLength: 128),
                        RequestUrl = c.String(maxLength: 128),
                        RequestData = c.String(),
                        UserAgent = c.String(maxLength: 128),
                        HttpMethod = c.String(maxLength: 16),
                        CreateUserId = c.Guid(nullable: false),
                        CreateUserCode = c.String(),
                        CreateUserName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ExceptionLogId);
            
            CreateTable(
                "dbo.Log_LoginLog",
                c => new
                    {
                        LoginLogId = c.Guid(nullable: false),
                        IpAddressName = c.String(maxLength: 128),
                        ServerHost = c.String(maxLength: 256),
                        ClientHost = c.String(maxLength: 256),
                        UserAgent = c.String(maxLength: 256),
                        OsVersion = c.String(maxLength: 64),
                        LoginTime = c.DateTime(nullable: false),
                        LoginOutTime = c.DateTime(),
                        StandingTime = c.Double(),
                        CreateUserId = c.Guid(nullable: false),
                        CreateUserCode = c.String(),
                        CreateUserName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LoginLogId);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MenuCode = c.String(maxLength: 32),
                        MenuName = c.String(maxLength: 32),
                        MenuUrl = c.String(maxLength: 32),
                        MenuIcon = c.String(maxLength: 32),
                        ParentId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUserCode = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUserCode = c.String(maxLength: 32),
                        Remark = c.String(maxLength: 200),
                        MerchantId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Log_OperateLog",
                c => new
                    {
                        OperationLogId = c.Guid(nullable: false),
                        ClientHost = c.String(maxLength: 128),
                        ServerHost = c.String(maxLength: 128),
                        RequestContentLength = c.Int(),
                        RequestType = c.String(maxLength: 16),
                        Url = c.String(maxLength: 512),
                        UrlReferrer = c.String(maxLength: 512),
                        RequestData = c.String(),
                        UserAgent = c.String(maxLength: 512),
                        ControllerName = c.String(maxLength: 64),
                        ActionName = c.String(maxLength: 128),
                        ActionExecutionTime = c.Double(nullable: false),
                        ResultExecutionTime = c.Double(nullable: false),
                        ResponseStatus = c.String(maxLength: 128),
                        Describe = c.String(),
                        CreateUserId = c.Guid(nullable: false),
                        CreateUserCode = c.String(),
                        CreateUserName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OperationLogId);
            
            CreateTable(
                "dbo.RoleMenu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUserCode = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUserCode = c.String(maxLength: 32),
                        Remark = c.String(maxLength: 200),
                        MerchantId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.MerchantInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MerchantInfo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        MerchantNo = c.String(nullable: false, maxLength: 32),
                        MerchantName = c.String(maxLength: 32),
                        MerchantPassword = c.String(nullable: false, maxLength: 32),
                        MerchantPhone = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.RoleMenu");
            DropTable("dbo.Log_OperateLog");
            DropTable("dbo.Menu");
            DropTable("dbo.Log_LoginLog");
            DropTable("dbo.Log_ExceptionLog");
            DropTable("dbo.Log_DataLog");
        }
    }
}
