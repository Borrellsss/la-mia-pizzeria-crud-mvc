namespace la_mia_pizzeria_static.Models.Repositories
{
    public class InMemoryCategoryRepository : IDbCategoryRepository
    {
        public List<Category> GetAll(bool pizzas)
        {
            throw new NotImplementedException();
        }
    }
}
