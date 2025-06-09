using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DAL.Models;
using UrlShortener.DAL.Models.Identities;

namespace UrlShortener.DAL.ApplicationDbContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ShortUrl> ShortUrls { get; set; }
        public DbSet<AboutInfo> AboutInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ShortUrl>()
                .HasIndex(s => s.ShortCode)
                .IsUnique();

            builder.Entity<ShortUrl>()
                .HasOne(s => s.CreatedByUser)
                .WithMany()
                .HasForeignKey(s => s.CreatedByUserId);

            builder.Entity<AboutInfo>()
                .HasOne(a => a.EditedByUser)
                .WithMany()
                .HasForeignKey(a => a.EditedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
