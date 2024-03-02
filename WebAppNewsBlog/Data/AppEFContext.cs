using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebAppNewsBlog.Data.Entities;

namespace WebAppNewsBlog.Data
{
    public class AppEFContext : DbContext
    {
        public AppEFContext(DbContextOptions<AppEFContext> options)
            : base(options)
        { }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<PostTagMapEntity> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PostTagMapEntity>(ur =>
            {
                ur.HasKey(ur => new { ur.PostId, ur.TagId });
            });

            builder.Entity<CategoryEntity>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<TagEntity>().HasIndex(u => u.Name).IsUnique();
        }
    }
}
