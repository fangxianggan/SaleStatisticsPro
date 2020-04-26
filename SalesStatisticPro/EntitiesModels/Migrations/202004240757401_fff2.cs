namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fff2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataBackup", "DBName", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DataBackup", "DBName");
        }
    }
}
