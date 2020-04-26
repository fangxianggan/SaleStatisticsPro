namespace EntitiesModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fff3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DataBackup", "BackUpName", c => c.String(maxLength: 64));
            AlterColumn("dbo.DataBackup", "Path", c => c.String(maxLength: 256));
            AlterColumn("dbo.DataBackup", "DBName", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DataBackup", "DBName", c => c.String(maxLength: 32));
            AlterColumn("dbo.DataBackup", "Path", c => c.String(maxLength: 32));
            AlterColumn("dbo.DataBackup", "BackUpName", c => c.String(maxLength: 32));
        }
    }
}
