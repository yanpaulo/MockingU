using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MockingU.Data.ValueConverters;
using System.Web;

namespace MockingU.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApiTemplate> ApiTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApiTemplate>()
                .Property(e => e.Methods)
                .HasConversion<StringToListValueConverter>();

            builder.Entity<ApiTemplate>()
                .OwnsOne(e => e.Response)
                .Property(e => e.Contents)
                .HasConversion<StringToListValueConverter>();
        }


    }
}