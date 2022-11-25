using la_mia_pizzeria_static.Models.FormModels;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public interface IDbPizzaRepository
    {
        void Create(Pizza pizzaToCreate);
        void Delete(Pizza pizzaToDelete);
        List<Pizza> GetAll(bool category, bool ingredients);
        Pizza GetById(int id, bool category, bool ingredients);
        Pizza GetLast(bool category, bool ingredients);
        void Update(Pizza pizzaToUpdate, PizzaForm pizzaFormData);
    }
}