namespace la_mia_pizzeria_static.Models.Repositories
{
    public interface IDbCategoryRepository
    {
        List<Category> GetAll(bool pizzas);
    }
}