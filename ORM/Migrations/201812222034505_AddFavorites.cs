namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavorites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Lots", t => t.LotId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.LotId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "LotId", "dbo.Lots");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.Users");
            DropIndex("dbo.Favorites", new[] { "LotId" });
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropTable("dbo.Favorites");
        }
    }
}
