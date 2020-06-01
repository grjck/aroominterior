namespace ARoomInterior.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elementreq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ElementObjs", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ElementObjs", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ElementObjs", "Description", c => c.String());
            AlterColumn("dbo.ElementObjs", "Name", c => c.String());
        }
    }
}
