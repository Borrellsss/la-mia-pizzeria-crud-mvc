using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Models.FormModels
{
    public class PizzaForm
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Ingredient>? Ingredients { get; set; }

        [BindProperty]
        public List<int>? SelectedIngredients { get; set; }
    }
}
