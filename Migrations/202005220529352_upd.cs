namespace ARoomInterior.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ElementObjs",
                c => new
                    {
                        ElementId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Preview = c.String(),
                        File = c.String(),
                    })
                .PrimaryKey(t => t.ElementId);
            
            CreateTable(
                "dbo.Invites",
                c => new
                    {
                        InviteId = c.Int(nullable: false, identity: true),
                        SenderId = c.String(maxLength: 128),
                        CurrentTeamId = c.Int(nullable: false),
                        CurrentTeam_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.InviteId)
                .ForeignKey("dbo.Teams", t => t.CurrentTeam_TeamId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.CurrentTeam_TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TeamId)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Speciallization = c.String(),
                        Info = c.String(),
                        CurrentTeamId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        CurrentTeam_TeamId = c.Int(),
                        Team_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.CurrentTeam_TeamId)
                .ForeignKey("dbo.Teams", t => t.Team_TeamId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.CurrentTeam_TeamId)
                .Index(t => t.Team_TeamId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                .PrimaryKey(t => t.ProjectElementId)
                .ForeignKey("dbo.ElementObjs", t => t.ElementObjElementId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId)
                .Index(t => t.ElementObjElementId)
                .Index(t => t.Project_ProjectId);
            
            CreateTable(
                "dbo.ProjectLawInfoes",
                c => new
                    {
                        ContractNumber = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ContractNumber);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Preview = c.String(),
                        CustomerId = c.String(maxLength: 128),
                        ProjectTeamId = c.Int(nullable: false),
                        LawInfoContractNumber = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .ForeignKey("dbo.ProjectLawInfoes", t => t.LawInfoContractNumber)
                .Index(t => t.CustomerId)
                .Index(t => t.LawInfoContractNumber);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Projects", "LawInfoContractNumber", "dbo.ProjectLawInfoes");
            DropForeignKey("dbo.ProjectElementObjs", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectElementObjs", "ElementObjElementId", "dbo.ElementObjs");
            DropForeignKey("dbo.Invites", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invites", "CurrentTeam_TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Team_TeamId", "dbo.Teams");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CurrentTeam_TeamId", "dbo.Teams");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Projects", new[] { "LawInfoContractNumber" });
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropIndex("dbo.ProjectElementObjs", new[] { "Project_ProjectId" });
            DropIndex("dbo.ProjectElementObjs", new[] { "ElementObjElementId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Team_TeamId" });
            DropIndex("dbo.AspNetUsers", new[] { "CurrentTeam_TeamId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Teams", new[] { "CustomerId" });
            DropIndex("dbo.Invites", new[] { "CurrentTeam_TeamId" });
            DropIndex("dbo.Invites", new[] { "SenderId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectLawInfoes");
            DropTable("dbo.ProjectElementObjs");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Teams");
            DropTable("dbo.Invites");
            DropTable("dbo.ElementObjs");
        }
    }
}
