using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public Pizza()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }
        public Pizza(string name, string description, string image, double price, bool isAvailable)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            Name = name;
            Description = description;
            Image = image;
            Price = price;
            IsAvailable = isAvailable;
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(30, ErrorMessage = "Il nome non più superare i 30 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatoria")]
        [StringLength(500, ErrorMessage = "La descrizione non più superare i 500 caratteri")]
        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Required(ErrorMessage = "L'immagine è obbligatoria")]
        [StringLength(255, ErrorMessage = "L'url dell'immagine non più superare i 255 caratteri")]
        [Url(ErrorMessage = "Devi inserire un url valido")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Il prezzo è obbligatorio")]
        [Range(0.10, Double.MaxValue, ErrorMessage = "Il prezzo non può essere minore di 0,10 euro")]
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
