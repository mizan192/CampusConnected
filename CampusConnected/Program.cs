using CampusConnected.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//registration connection string 
var provider = builder.Services.BuildServiceProvider();
var config=provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<StudentDBContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//middleware 

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
