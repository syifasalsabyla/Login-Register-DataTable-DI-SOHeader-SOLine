using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using latihanLogSig9.Models;

namespace latihanLogSig9.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<latihanLogSig9.Models.SOHeader> SOHeader { get; set; }
        public DbSet<latihanLogSig9.Models.SOLine> SOLine { get; set; }
        public DbSet<latihanLogSig9.Models.Member> Member { get; set; }
        public DbSet<latihanLogSig9.Models.Produk> Produk { get; set; }
    }
}
