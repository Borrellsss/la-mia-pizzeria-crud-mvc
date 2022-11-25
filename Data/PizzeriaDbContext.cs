using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data
{
    public class PizzeriaDbContext : DbContext
    {
        public static PizzeriaDbContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PizzeriaDbContext();
                }
                return _instance;
            }
        }
        private static PizzeriaDbContext _instance;
        private PizzeriaDbContext()
        {
            
        }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-pizzeria;Integrated Security=True; Encrypt=False");
        }
    }
}
