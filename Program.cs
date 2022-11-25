using la_mia_pizzeria_static.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbPizzaRepository, DbPizzaRepository>();
builder.Services.AddScoped<IDbCategoryRepository, DbCategoryRepository>();
builder.Services.AddScoped<IDbIngredientRepository, DbIngredientRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//inserito per il progetto
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
//inserito per il progetto

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
