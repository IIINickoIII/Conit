namespace Conit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntToStringPictureIdInPartEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Parts", "PictureId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Parts", "PictureId", c => c.Int(nullable: false));
        }
    }
}
