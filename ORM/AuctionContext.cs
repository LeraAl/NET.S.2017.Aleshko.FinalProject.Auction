using System.Data.Entity;

namespace ORM
{
    public class AuctionContext : DbContext
    {
        public AuctionContext()
            : base("name=AuctionContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<LotState> LotStates { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Lots)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lot>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Lot>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Lot>()
                .Property(e => e.StartPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Lot>()
                .Property(e => e.StartDatetime)
                .HasPrecision(0);

            modelBuilder.Entity<Lot>()
                .HasMany(e => e.Rates)
                .WithRequired(e => e.Lots)
                .HasForeignKey(e => e.LotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LotState>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<LotState>()
                .HasMany(e => e.Lots)
                .WithRequired(e => e.LotState)
                .HasForeignKey(e => e.StateId);

            modelBuilder.Entity<Rate>()
                .Property(e => e.Datetime)
                .HasPrecision(0);

            modelBuilder.Entity<Rate>()
                .Property(e => e.RateSize)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("User_to_Role").MapLeftKey("Role_Id").MapRightKey("User_Id"));

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Lots)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.OwnerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Rates)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
