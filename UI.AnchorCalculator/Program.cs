
using Core.AnchorCalculator.Entities;
using DAL.AnchorCalculator;
using DAL.AnchorCalculator.Cotracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UI.AnchorCalculator.Services;


var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<MaterialService>();
builder.Services.AddTransient<AnchorService>();
builder.Services.AddTransient<SvgMakingService>();
builder.Services.AddTransient<CalculateService>();


var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Anchor}/{action=Index}/{id?}");

app.Run();
