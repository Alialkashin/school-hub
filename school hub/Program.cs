using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using school_hub.Data;
using school_hub.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddIdentity<User, IdentityRole<int>>()
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<AppDBContext>();

builder.Services.AddRazorPages();
builder.Logging.AddConsole();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    AppDBContext context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    DbInitializer.Seed(context);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
//لتعديل واجهة واحدة
//dotnet aspnet-codegenerator identity -dc ApplicationDbContext --files "Account/Login"

