using Microsoft.EntityFrameworkCore;
using WebPizzaSite.Data;
using WebPizzaSite.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PizzaDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{ 
    var context = serviceScope.ServiceProvider.GetService<PizzaDbContext>();
    context?.Database.Migrate();

    if(!context.Products.Any())
    {
        var cat = context.Categories.FirstOrDefault();
        if (cat!=null)
        {
            var p = new ProductEntity
            {
                Category=cat,
                Name= "Ель-Капрічо",
                Price=155.00m
            };
            var pi1 = new ProductImageEntity
            {
                Name="1.webp",
                Priority=0,
                Product=p
            };
            var pi2 = new ProductImageEntity
            {
                Name="2.jpg",
                Priority=1,
                Product=p
            };
            context.Add(p);
            context.Add(pi1);
            context.Add(pi2);
            context.SaveChanges();
        }
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();
