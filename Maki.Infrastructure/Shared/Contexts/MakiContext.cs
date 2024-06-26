using Maki.Domain.Artisan.Models.Aggregates;
using Maki.Domain.IAM.Models.Entities;
using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace Maki.Infrastructure.Shared.Contexts;

public class MakiContext : DbContext
{
    public MakiContext()
    {
        
    }
    
    public MakiContext(DbContextOptions<MakiContext> options) : base(options)
    {
        
    }
    
    public DbSet<ProductA> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Domain.DesignRequest.Models.Entities.DesignRequest> DesignRequests { get; set; }
    public DbSet<Domain.Customer.Models.Entities.Customer> Customers { get; set; }
    public DbSet<ArtisanA> Artisans { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=viaduct.proxy.rlwy.net;Port=44024;Uid=root;Pwd=pmXLiWDTzdknUihnksNXaTcdKbLSnBYe;Database=MakiDB;", serverVersion);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ProductA>().ToTable("Product");
        builder.Entity<Category>().ToTable("Category");
        builder.Entity<Domain.Customer.Models.Entities.Customer>().ToTable("Customer");
        builder.Entity<ArtisanA>().ToTable("Artisan");
        
        builder.Entity<ProductA>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
        
        builder.Entity<User>().ToTable("User");

        // DesignRequest to Artisan relationship
        builder.Entity<Domain.DesignRequest.Models.Entities.DesignRequest>()
            .ToTable("DesignRequest")
            .HasOne(dr => dr.Artisan)
            .WithMany(a => a.DesignRequests)
            .HasForeignKey(dr => dr.ArtisanId);
    }

}