namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acdfdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SysConfig",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConfigId = c.Guid(nullable: false),
                        Code = c.String(maxLength: 32),
                        Value = c.String(maxLength: 3000),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUserCode = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        UpdateUserCode = c.String(maxLength: 32),
                        Remark = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SysConfig");
        }
    }
}
