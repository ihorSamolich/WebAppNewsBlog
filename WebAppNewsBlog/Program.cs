using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAppNewsBlog.AutoMapper;
using WebAppNewsBlog.Data;
using WebAppNewsBlog.Data.Entities.Identity;
using WebAppNewsBlog.Interfaces;
using WebAppNewsBlog.Interfaces.Repository;
using WebAppNewsBlog.Repositories;
using WebAppNewsBlog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppEFContext>(opt =>
               opt.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IPostTagRepository, PostTagRepository>();


// MapperConfiguration SETTINGS
builder.Services.AddScoped(provider => new MapperConfiguration(config =>
    {
        config.AddProfile(new AppMapProfile(provider.GetService<AppEFContext>()));
    }).CreateMapper());

// AddIdentity SETTINGS
builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Stores.MaxLengthForKeys = 128;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.SignIn.RequireConfirmedEmail = true;
})
  .AddEntityFrameworkStores<AppEFContext>()
  .AddDefaultTokenProviders();

// JWT SETTINGS
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSecretKey")));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters()
    {

        IssuerSigningKey = signinKey,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidateAudience = false,
        ValidateIssuer = false,
    };
});


var app = builder.Build();

app.UseCors(options =>
    options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseSwagger();
app.UseSwaggerUI();


// StaticFile Options
#region
var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
if (!Directory.Exists(dir))
{
    Directory.CreateDirectory(dir);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = "/images"
});
#endregion

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.SeedData();

app.Run();
