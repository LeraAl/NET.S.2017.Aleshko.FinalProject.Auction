using System.Data.Entity;

namespace ORM
{
    public class AuctionDatabaseInitializer : DropCreateDatabaseIfModelChanges<AuctionContext>
    {
        protected override void Seed(AuctionContext db)
        {
            db.LotStates.Add(new LotState() {Name = "Active"});
            db.LotStates.Add(new LotState() {Name = "Sold"});

            db.Roles.Add(new Role() {Name = "Administrator"});
            db.Roles.Add(new Role() {Name = "Moderator"});
            db.Roles.Add(new Role() {Name = "User"});
            db.Roles.Add(new Role() {Name = "Banned"});

            db.Categories.Add(new Category() {Name = "Clothing"});
            db.Categories.Add(new Category() {Name = "Electronics"});
            db.Categories.Add(new Category() {Name = "Toys"});
            db.Categories.Add(new Category() {Name = "Cars"});
        }
    }
}