using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

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
            List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
            return View(pizzas);
        }

        public IActionResult Details(int id)
        {
            Pizza pizza = db.Pizzas.Find(id);
            return View(pizza);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View(pizza);
            }

            db.Pizzas.Add(pizza);
            db.SaveChanges();

            Pizza _pizza = db.Pizzas.OrderByDescending(p => p.Id).First();

            return RedirectToAction("Details", new { id = _pizza.Id });
        }

        public IActionResult Update(int id)
        {
            Pizza pizza = db.Pizzas.Find(id);

            if(pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View(pizza);
            }

            Pizza pizzaToUpdate = db.Pizzas.Find(id);

            if(pizzaToUpdate == null)
            {
                return NotFound();
            }

            pizzaToUpdate.Name = pizza.Name;
            pizzaToUpdate.Description = pizza.Description;
            pizzaToUpdate.Image = pizza.Image;
            pizzaToUpdate.Price = pizza.Price;
            pizzaToUpdate.IsAvailable = pizza.IsAvailable;

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