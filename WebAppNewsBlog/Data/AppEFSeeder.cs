﻿using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAppNewsBlog.Constants;
using WebAppNewsBlog.Data.Entities;
using WebAppNewsBlog.Data.Entities.Identity;
using WebAppNewsBlog.Helpers;

namespace WebAppNewsBlog.Data
{
    public static class AppEFSeeder
    {
        public static async void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<AppEFContext>();

                context.Database.Migrate();

                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<UserEntity>>();

                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<RoleEntity>>();

                if (!context.Roles.Any())
                {
                    foreach (var role in Roles.All)
                    {
                        var result = roleManager.CreateAsync(new RoleEntity
                        {
                            Name = role
                        }).Result;
                    }
                }

                if (!context.Users.Any())
                {
                    var user = new UserEntity
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Email = "admin@gmail.com",
                        UserName = "admin@gmail.com"
                    };
                    var result = userManager.CreateAsync(user, "123456").Result;
                    if (result.Succeeded)
                    {
                        result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
                    }
                }

                if (!context.Categories.Any())
                {
                    var fakeCategory = new Faker<CategoryEntity>()
                        .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                        .RuleFor(c => c.UrlSlug, (f, c) => UrlSlugMaker.GenerateSlug(c.Name))
                        .RuleFor(c => c.Description, f => f.Lorem.Sentence());

                    List<CategoryEntity> fakeCategoryData = fakeCategory.Generate(10);

                    fakeCategoryData = fakeCategoryData
                        .GroupBy(category => category.Name)
                        .Select(group => group.First())
                        .ToList();

                    await context.Categories.AddRangeAsync(fakeCategoryData);
                    await context.SaveChangesAsync();
                }

                if (!context.Tags.Any())
                {
                    var fakeTag = new Faker<TagEntity>()
                        .RuleFor(c => c.Name, f => f.Lorem.Word())
                        .RuleFor(c => c.UrlSlug, (f, c) => UrlSlugMaker.GenerateSlug(c.Name))
                        .RuleFor(c => c.Description, f => f.Lorem.Sentence());

                    List<TagEntity> fakeTagData = fakeTag.Generate(50);

                    fakeTagData = fakeTagData
                        .GroupBy(tag => tag.Name)
                        .Select(group => group.First())
                        .Where(tag => tag.Name.Length > 2)
                        .ToList();

                    await context.Tags.AddRangeAsync(fakeTagData);
                    await context.SaveChangesAsync();
                }

                if (!context.Posts.Any())
                {
                    List<int> categoriesId = context.Categories.Select(c => c.Id).ToList();

                    var fakePost = new Faker<PostEntity>()
                        .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                        .RuleFor(p => p.ShortDescription, f => f.Lorem.Paragraph())
                        .RuleFor(p => p.Description, f => f.Lorem.Paragraphs(5))
                        .RuleFor(p => p.Meta, f => f.Lorem.Sentence(10))
                        .RuleFor(c => c.UrlSlug, (f, c) => UrlSlugMaker.GenerateSlug(c.Title))
                        .RuleFor(p => p.Published, f => f.Random.Bool())
                        .RuleFor(p => p.PostedOn, f => f.Date.Recent().ToUniversalTime())
                        .RuleFor(p => p.Modified, (f, p) => f.Date.Between(p.PostedOn, DateTime.UtcNow).ToUniversalTime())
                        .RuleFor(p => p.CategoryId, f => f.PickRandom(categoriesId));

                    List<PostEntity> fakePostData = fakePost.Generate(100);

                    fakePostData = fakePostData
                        .GroupBy(tag => tag.Title)
                        .Select(group => group.First())
                        .ToList();

                    await context.Posts.AddRangeAsync(fakePostData);
                    await context.SaveChangesAsync();
                }

                if (!context.PostTags.Any())
                {
                    List<int> postsId = context.Posts.Select(c => c.Id).ToList();
                    List<int> tagsId = context.Tags.Select(c => c.Id).ToList();

                    var fakePostTagMap = new Faker<PostTagMapEntity>()
                        .RuleFor(ptm => ptm.PostId, f => f.PickRandom(postsId))
                        .RuleFor(ptm => ptm.TagId, f => f.PickRandom(tagsId));

                    List<PostTagMapEntity> fakePostTagMapData = fakePostTagMap.Generate(500);

                    fakePostTagMapData = fakePostTagMapData
                        .GroupBy(ptm => new { ptm.PostId, ptm.TagId })
                        .Select(group => group.First())
                        .GroupBy(ptm => ptm.PostId)
                        .SelectMany(group => group.Take(5))
                        .ToList();

                    await context.PostTags.AddRangeAsync(fakePostTagMapData);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
