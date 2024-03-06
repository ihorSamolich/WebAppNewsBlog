using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebAppNewsBlog.Data.Entities;
using WebAppNewsBlog.Data.Entities.Identity;
using WebAppNewsBlog.Data.Entities.Images;

namespace WebAppNewsBlog.Data
{
    public class AppEFContext : IdentityDbContext<UserEntity, RoleEntity, int,
        IdentityUserClaim<int>, UserRoleEntity, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options)
            : base(options)
        { }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<PostTagMapEntity> PostTags { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRoleEntity>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });
                ur.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(r => r.RoleId)
                    .IsRequired();
                ur.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();
            });

            builder.Entity<PostTagMapEntity>(ur =>
            {
                ur.HasKey(ur => new { ur.PostId, ur.TagId });
            });

            builder.Entity<CategoryEntity>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<TagEntity>().HasIndex(u => u.Name).IsUnique();
        }
    }
}
