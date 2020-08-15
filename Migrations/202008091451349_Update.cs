namespace ARoomInterior.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "RoomID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "RoomID");
        }
    }
}
