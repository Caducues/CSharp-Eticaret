using Microsoft.EntityFrameworkCore;
using Test_Eticaret.Data;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages desteði ekle
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// MVC servisleri ekle
builder.Services.AddControllersWithViews();

// MySQL veritabaný baðlantýsýný yapýlandýr
string _GetConnStringName = builder.Configuration.GetConnectionString("DefaultConnectionMySQL");

// `DbContext`i havuzlama yerine, `AddDbContext` kullanýyoruz
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(_GetConnStringName, ServerVersion.AutoDetect(_GetConnStringName)));

var app = builder.Build();

// HTTP request pipeline yapýlandýrmasý
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
