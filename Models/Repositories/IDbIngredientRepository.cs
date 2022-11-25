namespace la_mia_pizzeria_static.Models.Repositories
{
    public interface IDbIngredientRepository
    {
        List<Ingredient> GetAll(bool pizzas);
        Ingredient GetById(int id, bool pizzas);
    }
}