namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SqlLog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SqlLogId = c.Guid(nullable: false),
                        OperateSql = c.String(),
                        EndDateTime = c.DateTime(nullable: false),
                        ElapsedTime = c.Double(nullable: false),
                        Parameter = c.String(),
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
            DropTable("dbo.SqlLog");
        }
    }
}
