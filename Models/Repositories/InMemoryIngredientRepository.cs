namespace la_mia_pizzeria_static.Models.Repositories
{
    public class InMemoryIngredientRepository : IDbIngredientRepository
    {
        public List<Ingredient> GetAll(bool pizzas)
        {
            throw new NotImplementedException();
        }

        public Ingredient GetById(int id, bool pizzas)
        {
            throw new NotImplementedException();
        }
    }
}
