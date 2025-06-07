using lucars.Controllers;
using lucars.Models;
using lucars.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

var connectionString=builder.Configuration.GetConnectionString("DefaulConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<ApplicationDbContext>(options =>
options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();



// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
