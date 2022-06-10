using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sever;
using Sever.Data;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);


var assembly = typeof(Program).Assembly.GetName().Name;

var defaultString = builder.Configuration.GetConnectionString("DefaultConnection");

if (seed)
{
    SeedData.EnsureSeedData(defaultString);
}

builder.Services.AddCors(confg =>
              confg.AddPolicy("AllowAll",
                  p => p.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()));




builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
{
    options.UseSqlServer(defaultString, b => b.MigrationsAssembly(assembly));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b =>
        b.UseSqlServer(defaultString, opt => opt.MigrationsAssembly(assembly));
    }
    ).AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
        b.UseSqlServer(defaultString, opt => opt.MigrationsAssembly(assembly));
    }
    ).AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

//builder.Services.ConfigureApplicationCookie(config =>
//{
//    config.Cookie.Name = "IdentityServer.Cookie";
//    config.LoginPath = "/Auth/Login";
//    config.LogoutPath = "/Auth/Logout";
//});



var app = builder.Build();
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
