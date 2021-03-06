namespace ORM.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class EditPasswordColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 20, unicode: false));
        }
    }
}
