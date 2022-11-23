using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(30, ErrorMessage = "Il nome non più superare i 30 caratteri")]
        public string Name { get; set; }
        public List<Pizza> Pizzas { get; set; }
    }
}
