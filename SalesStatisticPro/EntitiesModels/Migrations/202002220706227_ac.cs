namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ac : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 64),
                        NickName = c.String(maxLength: 64),
                        Gender = c.Int(nullable: false),
                        PhoneNumber = c.String(maxLength: 32),
                        ReceAddress = c.String(maxLength: 200),
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
            DropTable("dbo.UserInfo");
        }
    }
}
