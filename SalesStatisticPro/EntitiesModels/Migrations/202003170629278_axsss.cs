namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class axsss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MerchantInfo", "MerchantName", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MerchantInfo", "MerchantName", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
