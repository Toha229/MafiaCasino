using MafiaCasinoWeb.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using MafiaCasinoWeb.Infrastructure.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

ServicesConfiguration.Config(builder.Services);

AutoMapperConfiguration.Config(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

//if (DbAccess.Db.Users.Where(u => u.UserName == "Toha229").FirstOrDefault() == null)
//{
//    DbAccess.Db.Users.Add(new User()
//    {
//        UserName = "Toha229",
//        Email = "toha229orig@gmail.com",
//        Cash = 1000000,
//        EmailConfirmed = true,
//        PasswordHash = "123123"
//    });
//    DbAccess.Db.SaveChanges();
//}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "signin",
	pattern: "Home/SignIn",
	defaults: new { controller = "Account", action = "Login" });

app.Run();