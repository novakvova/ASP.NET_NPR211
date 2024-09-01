using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebPizzaSite.Constants;
using WebPizzaSite.Data;
using WebPizzaSite.Data.Entities;
using WebPizzaSite.Data.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PizzaDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity options
builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = true;

    //options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<PizzaDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<PizzaDbContext>();
    var userManager = serviceScope.ServiceProvider.GetService<UserManager<UserEntity>>();
    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<RoleEntity>>();

    context?.Database.Migrate();

    if (!context.Products.Any())
    {
        var cat = context.Categories.FirstOrDefault();
        if (cat != null)
        {
            var p = new ProductEntity
            {
                Category = cat,
                Name = "Ель-Капрічо",
                Price = 155.00m
            };
            var pi1 = new ProductImageEntity
            {
                Name = "1.webp",
                Priority = 0,
                Product = p
            };
            var pi2 = new ProductImageEntity
            {
                Name = "2.jpg",
                Priority = 1,
                Product = p
            };
            context.Add(p);
            context.Add(pi1);
            context.Add(pi2);
            context.SaveChanges();
        }
    }

    if (!context.Roles.Any())
    {
        var admin = new RoleEntity
        {
            Name = Roles.Admin
        };
        var result = roleManager.CreateAsync(admin).Result;
        if (!result.Succeeded)
        {
            Console.WriteLine($"------Помилка ствоерння ролі {Roles.Admin}------");
        }

        result = roleManager.CreateAsync(new RoleEntity { Name = Roles.User }).Result;
        if (!result.Succeeded)
        {
            Console.WriteLine($"------Помилка ствоерння ролі {Roles.User}------");
        }
    }

    if (!context.Users.Any())
    {
        var user = new UserEntity
        {
            Email = "amdin@gmail.com",
            UserName="admin@gmail.com",
            LastName="Шолом",
            FirstName="Вулкан",
            Picture="amdin.jpg"
        };
        var result = userManager.CreateAsync(user, "123456").Result;
        if(result.Succeeded)
        {
            result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
            if (!result.Succeeded)
            {
                Console.WriteLine($"-------Не вдалося надати роль {Roles.Admin} користувачу {user.Email}------");
            }
        }
        else
        {
            Console.WriteLine($"-------Не вдалося створити користувача {user.Email}-------");
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
