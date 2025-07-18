
namespace allspice_dotnet_fullstack.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients(id, created_at, updated_at, name, quantity, recipe_id)
    VALUES(@Id, @CreatedAt, @UpdatedAt, @Name, @Quantity, @RecipeId);
    
    SELECT ingredients.*, recipes.* FROM ingredients INNER JOIN recipes ON ingredients.recipe_id = recipes.id WHERE ingredients.id = LAST_INSERT_ID();";

    Ingredient createdIngredient = _db.Query(sql, (Ingredient ingredient, Recipe recipe) =>
    {
      ingredient.RecipeId = recipe.Id;
      return ingredient;
    }, ingredientData).SingleOrDefault();
    return createdIngredient;
  }
}