namespace ARoomInterior.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatespawnmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectElementObjs", "ElementObjElementId", "dbo.ElementObjs");
            DropForeignKey("dbo.ProjectElementObjs", "Project_ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectElementObjs", new[] { "ElementObjElementId" });
            DropIndex("dbo.ProjectElementObjs", new[] { "Project_ProjectId" });
            CreateTable(
                "dbo.SpawnModels",
                c => new
                    {
                        SpawnModelId = c.Int(nullable: false, identity: true),
                        ElementObjElementId = c.Int(nullable: false),
                        Project_ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.SpawnModelId)
                .ForeignKey("dbo.ElementObjs", t => t.ElementObjElementId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId)
                .Index(t => t.ElementObjElementId)
                .Index(t => t.Project_ProjectId);
            
            DropTable("dbo.ProjectElementObjs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProjectElementObjs",
                c => new
                    {
                        ProjectElementId = c.Int(nullable: false, identity: true),
                        CoordinateX = c.Single(nullable: false),
                        CoordinateY = c.Single(nullable: false),
                        CoordinateZ = c.Single(nullable: false),
                        ElementObjElementId = c.Int(nullable: false),
                        Project_ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectElementId);
            
            DropForeignKey("dbo.SpawnModels", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.SpawnModels", "ElementObjElementId", "dbo.ElementObjs");
            DropIndex("dbo.SpawnModels", new[] { "Project_ProjectId" });
            DropIndex("dbo.SpawnModels", new[] { "ElementObjElementId" });
            DropTable("dbo.SpawnModels");
            CreateIndex("dbo.ProjectElementObjs", "Project_ProjectId");
            CreateIndex("dbo.ProjectElementObjs", "ElementObjElementId");
            AddForeignKey("dbo.ProjectElementObjs", "Project_ProjectId", "dbo.Projects", "ProjectId");
            AddForeignKey("dbo.ProjectElementObjs", "ElementObjElementId", "dbo.ElementObjs", "ElementId", cascadeDelete: true);
        }
    }
}
