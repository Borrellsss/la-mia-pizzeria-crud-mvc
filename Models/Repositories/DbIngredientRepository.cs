using la_mia_pizzeria_static.Data;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbIngredientRepository
    {
        private PizzeriaDbContext db = PizzeriaDbContext.Instance;
        public List<Ingredient> GetAll(bool pizzas)
        {
            if (pizzas)
            {
                return db.Ingredients.Include("Pizzas").ToList();
            }

            return db.Ingredients.ToList();
        }
        public Ingredient GetById(int id, bool pizzas)
        {
            if (pizzas)
            {
                return db.Ingredients.Include("Pizzas").Where(p => p.Id == id).First();
            }

            return db.Ingredients.Where(p => p.Id == id).First();
        }
    }
}
