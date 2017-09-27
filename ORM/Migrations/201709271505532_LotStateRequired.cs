namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LotStateRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lots", "StateId", "dbo.LotStates");
            DropIndex("dbo.Lots", new[] { "StateId" });
            AlterColumn("dbo.Lots", "StateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lots", "StateId");
            AddForeignKey("dbo.Lots", "StateId", "dbo.LotStates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "StateId", "dbo.LotStates");
            DropIndex("dbo.Lots", new[] { "StateId" });
            AlterColumn("dbo.Lots", "StateId", c => c.Int());
            CreateIndex("dbo.Lots", "StateId");
            AddForeignKey("dbo.Lots", "StateId", "dbo.LotStates", "Id");
        }
    }
}
