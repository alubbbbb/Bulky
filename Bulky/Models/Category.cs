using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        // Primärschlüssel für die Kategorie (eindeutige ID)
        [Key]
        public int Id { get; set; }

        // Name der Kategorie (Pflichtfeld, max. 30 Zeichen, im Formular als "Category Name" angezeigt)
        [Required] // Muss ausgefüllt werden
        [MaxLength(30)] // Maximal 30 Zeichen erlaubt
        [DisplayName("Category Name")] // Anzeigename im Formular
        public string Name { get; set; }

        // Reihenfolge, in der die Kategorie angezeigt wird (im Formular als "Display Order" angezeigt, Wert muss zwischen 1 und 100 liegen)
        [DisplayName("Display Order")] // Anzeigename im Formular
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")] // Wertbereich 1-100 mit Fehlermeldung
        public int DisplayOrder { get; set; }
    }
}
