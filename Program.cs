using Microsoft.EntityFrameworkCore;
using RecycLab.Models;

var builder = WebApplication.CreateBuilder(args);

var Cnction = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<RecycLabContext>(options =>
    options.UseMySql(Cnction, ServerVersion.AutoDetect(Cnction)));

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession();

builder.Services.AddMvc().AddSessionStateTempDataProvider();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

}
app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

