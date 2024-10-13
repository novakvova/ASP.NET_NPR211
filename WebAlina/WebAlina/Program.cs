using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Net.WebSockets;
using WebAlina.Data;
using WebAlina.Data.Entities;
using WebAlina.Interfaces;
using WebAlina.Mapper;
using WebAlina.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AlinaDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IImageHulk, ImageHulk>();

builder.Services.AddAutoMapper(typeof(AppMapProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(opt=>
    opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

var dirImage = builder.Configuration["ImageFolder"] ?? "uploading";
var dirPath = Path.Combine(Directory.GetCurrentDirectory(), dirImage);
if (!Directory.Exists(dirPath))
    Directory.CreateDirectory(dirPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dirPath),
    RequestPath = "/images"
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AlinaDbContext>();
    var imageHulk = scope.ServiceProvider.GetRequiredService<IImageHulk>();
    //dbContext.Database.EnsureDeleted();
    dbContext.Database.Migrate();

    if (dbContext.Categories.Count() == 0)
    {
        int number = 10;
        var list = new Faker("uk")
            .Commerce.Categories(number);
        foreach (var name in list)
        {
            string image = imageHulk.Save("https://picsum.photos/1200/800?category").Result;
            var cat = new CategoryEntity
            {
                Name = name,
                Description = new Faker("uk").Commerce.ProductDescription(),
                Image = image
            };
            dbContext.Categories.Add(cat);
            dbContext.SaveChanges();
        }
    }

    if (dbContext.Products.Count() == 0)
    {
        var categories = dbContext.Categories.ToList();

        var fakerProduct = new Faker<ProductEntity>("uk")
            .RuleFor(u => u.Name, (f, u) => f.Commerce.Product())
            .RuleFor(u => u.Price, (f, u) => decimal.Parse(f.Commerce.Price()))
            .RuleFor(u => u.Category, (f, u) => f.PickRandom(categories));

        string url = "https://picsum.photos/1200/800?product";

        var products = fakerProduct.GenerateLazy(100);
        Random r = new Random();

        foreach (var product in products)
        {
            dbContext.Add(product);
            dbContext.SaveChanges();
            int imageCount = r.Next(3, 5);
            for (int i = 0; i < imageCount; i++)
            {
                var imageName = imageHulk.Save(url).Result;
                var imageProduct = new ProductImageEntity
                {
                    Product = product,
                    Image = imageName,
                    Priority = i
                };
                dbContext.Add(imageProduct);
                dbContext.SaveChanges();
            }
        }
    }
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
