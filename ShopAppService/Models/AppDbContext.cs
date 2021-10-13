using Microsoft.EntityFrameworkCore;
using ShopAppService.Models;
using System;

namespace ShopAppService.Models
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Category> Categories { get; set; }
    }
}
