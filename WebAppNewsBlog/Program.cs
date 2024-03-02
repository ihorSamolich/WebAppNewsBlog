using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAppNewsBlog.AutoMapper;
using WebAppNewsBlog.Data;
using WebAppNewsBlog.Interfaces.Repository;
using WebAppNewsBlog.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppEFContext>(opt =>
               opt.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();


builder.Services.AddScoped(provider => new MapperConfiguration(config =>
    {
        config.AddProfile(new AppMapProfile(provider.GetService<AppEFContext>()));
    }
).CreateMapper());


var app = builder.Build();

app.UseCors(options =>
    options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.SeedData();



app.Run();
