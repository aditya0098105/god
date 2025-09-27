using EarthDrive.Data;
using EarthDrive.Seeding;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EarthDriveContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EarthDriveContext") ?? throw new InvalidOperationException("Connection string 'EarthDriveContext' not found.")));

//Authentication sustem for Login and Logout fucntions (Chris 22500937)
//Will auto logout a user after 30 mins of inactivity
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/UserAuthorizations/Login";
        options.LogoutPath = "/UserAuthorizations/Logout"; 
        //Set Experation time of Loggedin User
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        //SlidingExperation allows the timer to reset and not logout a active user
        options.SlidingExpiration = true;
    });

builder.Services.AddSession(); 

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedTranscriptTable.Initilize(services); 
    SeedAuthorizationTable.Initilize(services);
    CustomerSeedData.Initialize(services);
    SeedingCars.Initialize(services);



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

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
