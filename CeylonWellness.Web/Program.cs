using CeylonWellness.Data;
using CeylonWellness.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.Security.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

#region External Authentication

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = "306909278130-13sttfr5ujqm9h047ic2h6rlmfu8nne9.apps.googleusercontent.com";
        options.ClientSecret = "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";
    })
    .AddFacebook(options =>
    {
        options.AppId = "your_facebook_app_id";
        options.AppSecret = "your_facebook_app_secret";
    });

#endregion

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
//app.MapRazorPages();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
