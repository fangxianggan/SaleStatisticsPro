namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zxx12 : DbMigration
    {
        public override void Up()
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
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MerchantInfo");
        }
    }
}
