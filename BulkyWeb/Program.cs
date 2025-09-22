using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

// Erstelle Builder-Objekt f�r die Konfiguration der Anwendung
var builder = WebApplication.CreateBuilder(args);

// F�ge MVC-Service und Datenbank-Kontext in den DI-Container ein
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

// Erstelle das WebApplication-Objekt
var app = builder.Build();

// Konfiguriere Fehlerbehandlung und Sicherheit f�r Produktionsumgebung
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Standard-HSTS Wert ist 30 Tage. Mehr Infos siehe https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Aktiviere HTTPS, statische Dateien und Routing/Autorisierung
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization(); 

// Definiere Standardroute f�r Controller und Actions
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

// Starte die Anwendung
app.Run();