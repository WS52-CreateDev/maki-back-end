using Maki.Domain.IAM.Models.Entities;
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
    
    public DbSet<User> Users { get; set; }
    
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
        
        builder.Entity<User>().ToTable("User");
    }
}