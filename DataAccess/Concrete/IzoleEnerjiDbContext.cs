using Core.Utilities;
using Entities.Concrete.DBModels;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class IzoleEnerjiDbContext : DbContext
    {
        private static string ConnectionString { get; set; }
        public IzoleEnerjiDbContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Premium> Premiums { get; set; }
        public DbSet<PremiumMode> PremiumModes { get; set; }
        public static void SetConnectionString(string connectionString)
        {
            if (ConnectionString == null)
            {
                ConnectionString = connectionString;
            }
            else
            {
                throw new ArgumentNullException("c", "ConnectionString");
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                        .UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<UserDetail>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserDetails)
                .HasForeignKey(ul => ul.UserId);

            modelBuilder.Entity<UserDetail>()
                .HasOne(ul => ul.PremiumMode)
                .WithMany(pm => pm.UserDetails)
                .HasForeignKey(ul => ul.PremiumModeId);
        }

        public void SeedInitialData()
        {
            var dataSeeds = new DataSeeds();
            int i = 0;
            foreach (var categoryData in dataSeeds.categories)
            {
                var category = Categories.FirstOrDefault(c => c.CategoryName == categoryData.CategoryName);
                if (category == null)
                {
                    category = new Category
                    {
                        CategoryName = categoryData.CategoryName,
                        Id = 0
                    };
                    Categories.Add(category);
                    SaveChanges();
                }
                foreach (var productData in dataSeeds.types[i])
                {
                    if (!Products.Any(p => p.EntityName == productData.name))
                    {
                        Products.Add(new Product
                        {
                            EntityName = productData.name,
                            RValue = (decimal)productData.r,
                            Price = (decimal)productData.price,
                            PriceChangeDate = DateTime.Now,
                            Unit = Units.SquareMeter,
                            CategoryId = category.Id,
                            Id = 0,
                        });
                    }
                }
                SaveChanges();
                i++;
            }
            foreach (var premium in dataSeeds.premiums)
            {
                if (!Premiums.Any(p => p.PremiumName == premium.PremiumName))
                {
                    Premiums.Add(premium);
                    SaveChanges();

                    var premiumModes = dataSeeds.premiumModes.Select(x =>
                    {
                        x.PremiumId = premium.Id;
                        return x;
                    }).ToList();
                    PremiumModes.AddRange(premiumModes);
                    SaveChanges();
                }

            }


        }
    }

}
