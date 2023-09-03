using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using URL_shortening_Service.Models.Entities;

namespace URL_shortening_Service.Models.Database
{
    public class UrlDataDbContext : DbContext
    {
        public UrlDataDbContext(DbContextOptions<UrlDataDbContext> options) : base(options)
        {
        }
        public DbSet<ShortenedUrl> ShortenedUrl { get; set; } = null!; // create table model

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder => 
            {
                builder.Property(s => s.Code).HasMaxLength(6); // max character length is 6 for unique code
                builder.HasIndex(s => s.Code).IsUnique(); // unique code generating 
            });
        }
    }
}