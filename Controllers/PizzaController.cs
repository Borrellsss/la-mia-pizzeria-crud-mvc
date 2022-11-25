using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.FormModels;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private DbPizzaRepository dbPizzaRepository;
        private DbCategoryRepository dbCategoryRepository;
        private DbIngredientRepository dbIngredientRepository;
        public PizzaController()
        {
            dbPizzaRepository = new DbPizzaRepository();
            dbCategoryRepository = new DbCategoryRepository();
            dbIngredientRepository = new DbIngredientRepository();
        }
        public IActionResult Index()
        {
            List<Pizza> pizzas = dbPizzaRepository.GetAll(true, true);
            return View(pizzas);
        }
        public IActionResult Details(int id)
        {
            Pizza pizza = dbPizzaRepository.GetById(id, true, true);
            return View(pizza);
        }
        public IActionResult Create()
        {
            PizzaForm pizzaFormData = new PizzaForm();
            pizzaFormData.Categories = dbCategoryRepository.GetAll(false);
            pizzaFormData.Ingredients = dbIngredientRepository.GetAll(false);
            pizzaFormData.SelectedIngredients = new List<int>();
            return View(pizzaFormData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm pizzaFormData)
        {
            if (!ModelState.IsValid)
            {
                pizzaFormData.Categories = dbCategoryRepository.GetAll(false);
                pizzaFormData.Ingredients = dbIngredientRepository.GetAll(false);

                if (pizzaFormData.SelectedIngredients == null)
                {
                    pizzaFormData.SelectedIngredients = new List<int>();
                }

                return View(pizzaFormData);
            }

            pizzaFormData.Pizza.Ingredients = new List<Ingredient>();

            if (pizzaFormData.SelectedIngredients.Count() > 0)
            {
                foreach (int IngredientId in pizzaFormData.SelectedIngredients)
                {
                    Ingredient selectedIngredient = dbIngredientRepository.GetById(IngredientId, false);
                    pizzaFormData.Pizza.Ingredients.Add(selectedIngredient);
                }
            }

            dbPizzaRepository.Create(pizzaFormData.Pizza);

            Pizza _pizza = dbPizzaRepository.GetLast(false, false);

            return RedirectToAction("Details", new { id = _pizza.Id });
        }

        public IActionResult Update(int id)
        {
            PizzaForm pizzaFormData = new PizzaForm();
            pizzaFormData.Pizza = dbPizzaRepository.GetById(id, false, true);

            if (pizzaFormData.Pizza == null)
            {
                return NotFound();
            }

            pizzaFormData.Categories = dbCategoryRepository.GetAll(false);
            pizzaFormData.Ingredients = dbIngredientRepository.GetAll(false);
            pizzaFormData.SelectedIngredients = new List<int>();

            foreach (Ingredient ingredient in pizzaFormData.Pizza.Ingredients)
            {
                pizzaFormData.SelectedIngredients.Add(ingredient.Id);
            }

            return View(pizzaFormData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm pizzaFormData)
        {
            Pizza pizzaToUpdate = dbPizzaRepository.GetById(id, false, true);

            if (!ModelState.IsValid)
            {
                pizzaFormData.Pizza = pizzaToUpdate;
                pizzaFormData.Categories = dbCategoryRepository.GetAll(false);
                pizzaFormData.Ingredients = dbIngredientRepository.GetAll(false);

                if (pizzaFormData.SelectedIngredients == null)
                {
                    pizzaFormData.SelectedIngredients = new List<int>();
                }

                return View(pizzaFormData);
            }

            if(pizzaToUpdate == null)
            {
                return NotFound();
            }

            dbPizzaRepository.Update(pizzaToUpdate, pizzaFormData); 

            return RedirectToAction("Details", new { id = pizzaToUpdate.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizzaToDelete = dbPizzaRepository.GetById(id, false, false);

            if (pizzaToDelete == null)
            {
                return NotFound();
            }

            dbPizzaRepository.Delete(pizzaToDelete);

            return RedirectToAction("Index");
        }
    }
}