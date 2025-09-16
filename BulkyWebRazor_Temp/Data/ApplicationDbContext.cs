using BulkyWebRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazor_Temp.Data
{
    public class ApplicationDbContext : DbContext
    {   // Konstruktor, der die Optionen für den DbContext entgegennimmt und an die Basisklasse weitergibt.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        // DbSet für die Kategorie-Tabelle, damit man mit Kategorien in der Datenbank arbeiten kann.

        public DbSet<Category> Categories { get; set; }
        // Methode zum Konfigurieren des Modells und zum Seed-Daten hinzufügen.
        // Diese Methode wird beim Erstellen des Datenbankmodells aufgerufen.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed-Daten für die Kategorie-Tabelle hinzufügen.
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
        }


    }
}
