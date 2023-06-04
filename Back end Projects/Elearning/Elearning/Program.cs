using Elearning.Models.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
Tools.SetConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddControllersWithViews();
builder.Services.AddSession(); 

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); // Add session middleware to the HTTP request pipeline

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
Tools.SetConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"));
