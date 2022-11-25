using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.FormModels;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzaRepository
    {
        private PizzeriaDbContext db = PizzeriaDbContext.Instance;
        public List<Pizza> GetAll(bool category, bool ingredients)
        {
            if(category && ingredients)
            {
                return db.Pizzas.Include("Category").Include("Ingredients").ToList();
            }
            else if(category)
            {
                return db.Pizzas.Include("Category").ToList();
            }
            else if(ingredients)
            {
                return db.Pizzas.Include("Ingredients").ToList();
            }

            return db.Pizzas.ToList();
        }

        public Pizza GetById(int id, bool category, bool ingredients)
        {
            if (category && ingredients)
            {
                return db.Pizzas.Include("Category").Include("Ingredients").Where(p => p.Id == id).First();
            }
            else if (category)
            {
                return db.Pizzas.Include("Category").Where(p => p.Id == id).First();
            }
            else if (ingredients)
            {
                return db.Pizzas.Include("Ingredients").Where(p => p.Id == id).First();
            }
            
            return db.Pizzas.Where(p => p.Id == id).First();
        }
        public Pizza GetLast(bool category, bool ingredients)
        {
            if (category && ingredients)
            {
                return db.Pizzas.Include("Category").Include("Ingredients").OrderBy(p => p.Id).Last();
            }
            else if (category)
            {
                return db.Pizzas.Include("Category").OrderBy(p => p.Id).Last();
            }
            else if (ingredients)
            {
                return db.Pizzas.Include("Ingredients").OrderBy(p => p.Id).Last();
            }

            return db.Pizzas.OrderBy(p => p.Id).Last();
        }
        public void Create(Pizza pizzaToCreate)
        {
            db.Pizzas.Add(pizzaToCreate);
            db.SaveChanges();
        }
        public void Update(Pizza pizzaToUpdate, PizzaForm pizzaFormData)
        {
            pizzaToUpdate.Name = pizzaFormData.Pizza.Name;
            pizzaToUpdate.Description = pizzaFormData.Pizza.Description;
            pizzaToUpdate.Image = pizzaFormData.Pizza.Image;
            pizzaToUpdate.Price = pizzaFormData.Pizza.Price;
            pizzaToUpdate.IsAvailable = pizzaFormData.Pizza.IsAvailable;
            pizzaToUpdate.CategoryId = pizzaFormData.Pizza.CategoryId;

            if (pizzaToUpdate.Ingredients != null)
            {
                pizzaToUpdate.Ingredients.Clear();
            }
            else
            {
                pizzaToUpdate.Ingredients = new List<Ingredient>();
            }

            if (pizzaFormData.SelectedIngredients != null)
            {
                foreach (int IngredientId in pizzaFormData.SelectedIngredients)
                {
                    Ingredient selectedIngredient = db.Ingredients.Find(IngredientId);
                    pizzaToUpdate.Ingredients.Add(selectedIngredient);
                }
            }

            db.SaveChanges();
        }

        public void Delete(Pizza pizzaToDelete)
        {
            db.Pizzas.Remove(pizzaToDelete);
            db.SaveChanges();
        }
    }
}
