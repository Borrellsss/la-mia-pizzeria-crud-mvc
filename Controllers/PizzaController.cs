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
    }
}
