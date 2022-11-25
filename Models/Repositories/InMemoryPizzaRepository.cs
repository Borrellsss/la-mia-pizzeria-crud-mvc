using Azure;
using la_mia_pizzeria_static.Models.FormModels;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class InMemoryPizzaRepository : IDbPizzaRepository
    {
        public static List<Pizza> Pizzas = new List<Pizza>();
        public List<Pizza> GetAll(bool category, bool ingredients)
        {
            return Pizzas;
        }
        public Pizza GetById(int id, bool category, bool ingredients)
        {
            throw new NotImplementedException();
        }
        public Pizza GetLast(bool category, bool ingredients)
        {
            throw new NotImplementedException();
        }
        public void Create(Pizza pizzaToCreate)
        {
            pizzaToCreate.Id = Pizzas.Count() + 1;
            //List<Category> categories = 
            //pizzaToCreate.Category = new Category() {};

        }
        public void Update(Pizza pizzaToUpdate, PizzaForm pizzaFormData)
        {
            throw new NotImplementedException();
        }
        public void Delete(Pizza pizzaToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
