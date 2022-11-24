using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.FormModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private PizzeriaDbContext db;
        public PizzaController()
        {
            db = new PizzeriaDbContext();
        }
        public IActionResult Index()
        {
            List<Pizza> pizzas = db.Pizzas.Include("Category").Include("Ingredients").ToList<Pizza>();
            return View(pizzas);
        }

        public IActionResult Details(int id)
        {
            Pizza pizza = db.Pizzas.Include("Category").Include("Ingredients").Where(p => p.Id == id).First();
            return View(pizza);
        }
        public IActionResult Create()
        {
            PizzaForm pizzaFormData = new PizzaForm();
            pizzaFormData.Categories = db.Categories.ToList<Category>();
            pizzaFormData.Ingredients = db.Ingredients.ToList<Ingredient>();
            pizzaFormData.SelectedIngredients = new List<int>();
            return View(pizzaFormData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm pizzaFormData)
        {
            if (!ModelState.IsValid)
            {
                pizzaFormData.Categories = db.Categories.ToList<Category>();
                pizzaFormData.Ingredients = db.Ingredients.ToList<Ingredient>();

                if(pizzaFormData.SelectedIngredients == null)
                {
                    pizzaFormData.SelectedIngredients = new List<int>();
                }

                return View(pizzaFormData);
            }

            pizzaFormData.Pizza.Ingredients = new List<Ingredient>();

            foreach (int IngredientId in pizzaFormData.SelectedIngredients)
            {
                Ingredient selectedIngredient = db.Ingredients.Find(IngredientId);
                pizzaFormData.Pizza.Ingredients.Add(selectedIngredient);
            }
            
            db.Pizzas.Add(pizzaFormData.Pizza);
            db.SaveChanges();

            Pizza _pizza = db.Pizzas.OrderBy(p => p.Id).Last();

            return RedirectToAction("Details", new { id = _pizza.Id });
        }

        public IActionResult Update(int id)
        {
            PizzaForm pizzaFormData = new PizzaForm();
            pizzaFormData.Pizza = db.Pizzas.Find(id);
            pizzaFormData.Categories = db.Categories.ToList<Category>();

            if (pizzaFormData.Pizza == null)
            {
                return NotFound();
            }

            return View(pizzaFormData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm pizzaFormData)
        {
            if (!ModelState.IsValid)
            {
                pizzaFormData.Pizza = db.Pizzas.Find(id);
                pizzaFormData.Categories = db.Categories.ToList<Category>();
                return View(pizzaFormData);
            }

            Pizza pizzaToUpdate = db.Pizzas.Find(id);

            if(pizzaToUpdate == null)
            {
                return NotFound();
            }

            pizzaToUpdate.Name = pizzaFormData.Pizza.Name;
            pizzaToUpdate.Description = pizzaFormData.Pizza.Description;
            pizzaToUpdate.Image = pizzaFormData.Pizza.Image;
            pizzaToUpdate.Price = pizzaFormData.Pizza.Price;
            pizzaToUpdate.IsAvailable = pizzaFormData.Pizza.IsAvailable;
            pizzaToUpdate.CategoryId = pizzaFormData.Pizza.CategoryId;

            db.SaveChanges();

            return RedirectToAction("Details", new { id = pizzaToUpdate.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizzaToDelete = db.Pizzas.Find(id);

            if (pizzaToDelete == null)
            {
                return NotFound();
            }

            db.Pizzas.Remove(pizzaToDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}