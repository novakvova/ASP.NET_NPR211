using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Net.WebSockets;
using WebAlina.Data;
using WebAlina.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AlinaDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    //dbContext.Database.EnsureDeleted();
    dbContext.Database.Migrate();

    if (!dbContext.Categories.Any())
    {
        var entity = new CategoryEntity
        {
            Name = "ќл≥мп≥йськ≥ ≥гри",
            Description = "—порт - це дуже круто.",
            Image = "alina.jpg"
        };
        dbContext.Categories.Add(entity);
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
