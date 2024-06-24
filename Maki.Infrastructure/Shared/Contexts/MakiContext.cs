﻿using Maki.Domain.IAM.Models.Entities;
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
    public DbSet<Domain.Customer.Models.Queries.Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=127.0.0.1,3306;Uid=root;Pwd=1234;Database=Maki;", serverVersion);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ProductA>().ToTable("Product");
        builder.Entity<Category>().ToTable("Category");
        builder.Entity<Domain.Customer.Models.Queries.Customer>().ToTable("Customer");
        
        builder.Entity<ProductA>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
        
        builder.Entity<User>().ToTable("User");
    }

}