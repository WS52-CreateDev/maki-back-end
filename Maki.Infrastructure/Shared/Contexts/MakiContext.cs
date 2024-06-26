﻿using Microsoft.EntityFrameworkCore;

namespace Maki.Infrastructure.Shared.Contexts;

public class MakiContext : DbContext
{
    public MakiContext()
    {
        
    }
    
    public MakiContext(DbContextOptions<MakiContext> options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=viaduct.proxy.rlwy.net;Port=44024;Uid=root;Pwd=pmXLiWDTzdknUihnksNXaTcdKbLSnBYe;Database=MakiDB;", serverVersion);
        }
    }

}