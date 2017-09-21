namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45, unicode: false),
                        Price = c.Decimal(nullable: false, precision: 10, scale: 2),
                        CategoryId = c.Int(nullable: false),
                        StateId = c.Int(),
                        Image = c.Binary(),
                        Description = c.Binary(),
                        OwnerId = c.Int(nullable: false),
                        StartPrice = c.Decimal(nullable: false, precision: 10, scale: 2),
                        StartDatetime = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LotStates", t => t.StateId)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.StateId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.LotStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LotId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Datetime = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Rate = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Lots", t => t.LotId)
                .Index(t => t.LotId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 25, unicode: false),
                        Password = c.String(nullable: false, maxLength: 20, unicode: false),
                        FirstName = c.String(nullable: false, maxLength: 45, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 45, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User_to_Role",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Rates", "LotId", "dbo.Lots");
            DropForeignKey("dbo.User_to_Role", "User_Id", "dbo.Users");
            DropForeignKey("dbo.User_to_Role", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Rates", "UserId", "dbo.Users");
            DropForeignKey("dbo.Lots", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.Lots", "StateId", "dbo.LotStates");
            DropIndex("dbo.User_to_Role", new[] { "User_Id" });
            DropIndex("dbo.User_to_Role", new[] { "Role_Id" });
            DropIndex("dbo.Rates", new[] { "UserId" });
            DropIndex("dbo.Rates", new[] { "LotId" });
            DropIndex("dbo.Lots", new[] { "OwnerId" });
            DropIndex("dbo.Lots", new[] { "StateId" });
            DropIndex("dbo.Lots", new[] { "CategoryId" });
            DropTable("dbo.User_to_Role");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Rates");
            DropTable("dbo.LotStates");
            DropTable("dbo.Lots");
            DropTable("dbo.Categories");
        }
    }
}
