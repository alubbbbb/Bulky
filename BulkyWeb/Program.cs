using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;

// Erstelle Builder-Objekt f�r die Konfiguration der Anwendung
var builder = WebApplication.CreateBuilder(args);

// F�ge MVC-Service und Datenbank-Kontext in den DI-Container ein
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


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
app.MapRazorPages();

// Definiere Standardroute f�r Controller und Actions
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

// Starte die Anwendung
app.Run();