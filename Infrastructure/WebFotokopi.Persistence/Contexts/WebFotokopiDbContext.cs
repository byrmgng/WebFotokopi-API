using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Domain.Entities;
using WebFotokopi.Domain.Entities.Commons;
using WebFotokopi.Domain.Entities.Identity;

namespace WebFotokopi.Persistence.Contexts
{
    public class WebFotokopiDbContext : DbContext
    {
        public WebFotokopiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<AppSeller> AppSellers { get; set; }
        public DbSet<SellerAddress> SellerAddresses { get; set; }
        public DbSet<SheetsPerPage> SheetsPerPages { get; set; }
        public DbSet<PaperSize> PaperSizes { get; set; }
        public DbSet<PaperType> PaperTypes { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<AppCustomer> AppCustomers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WebFotokopi.Domain.Entities.File> Files { get; set; }
        public DbSet<Order> Orders { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entriesDatas = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entriesDatas)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasKey(x => x.ID);
            modelBuilder.Entity<City>()
                .Property(x => x.ID).ValueGeneratedNever();
            modelBuilder.Entity<District>()
                .HasOne(x => x.City)
                .WithMany(x => x.Districts)
                .HasForeignKey(x => x.CityID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SellerAddress>()
                .HasOne(x => x.District)
                .WithMany(x => x.SellerAddress)
                .HasForeignKey(x => x.DistrictID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AppSeller>()
                .HasOne(x => x.SellerAddress)
                .WithOne(x => x.AppSeller)
                .HasForeignKey<AppSeller>(x => x.SellerAddressID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CustomerAddress>()
                .HasOne(x => x.District)
                .WithMany(x => x.CustomerAddresses)
                .HasForeignKey(x => x.DistrictID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AppCustomer>()
                .HasOne(x => x.CustomerAddress)
                .WithOne(x => x.AppCustomer)
                .HasForeignKey<AppCustomer>(x => x.CustomerAddressID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Package>()
                .HasOne(x => x.SheetsPerPage)
                .WithMany(x => x.ProductFeatures)
                .HasForeignKey(x => x.SheetsPerPageID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Package>()
                .HasOne(x => x.PaperSize)
                .WithMany(x => x.ProductFeatures)
                .HasForeignKey(x => x.PaperSizeID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Package>()
                .HasOne(x => x.PaperType)
                .WithMany(x => x.ProductFeatures)
                .HasForeignKey(x => x.PaperTypeID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasOne(x => x.Package)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.PackageID).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Order)
                .HasForeignKey(x=>x.OrderID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AppCustomer>()
                .HasMany(x=>x.Orders)
                .WithOne(x=>x.AppCustomer)
                .HasForeignKey(x=>x.CustomerID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AppSeller>()
                .HasMany(x => x.Packages)
                .WithOne(x => x.AppSeller)
                .HasForeignKey(x => x.SellerID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasOne(x => x.File)
                .WithMany(x => x.Products)
                .HasForeignKey(x=>x.FileID).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .HasOne(x => x.AppSeller)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.SellerID).OnDelete(DeleteBehavior.Restrict);
        }




    }
}
