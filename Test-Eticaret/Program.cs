using Microsoft.EntityFrameworkCore;
using Test_Eticaret.Data;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages deste�i ekle
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// MVC servisleri ekle
builder.Services.AddControllersWithViews();

// MySQL veritaban� ba�lant�s�n� yap�land�r
string _GetConnStringName = builder.Configuration.GetConnectionString("DefaultConnectionMySQL");

// `DbContext`i havuzlama yerine, `AddDbContext` kullan�yoruz
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(_GetConnStringName, ServerVersion.AutoDetect(_GetConnStringName)));

var app = builder.Build();

// HTTP request pipeline yap�land�rmas�
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
