
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


  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = @"
    SELECT * FROM ingredients WHERE ingredients.id = @IngredientId;";

    Ingredient ingredient = _db.Query<Ingredient>(sql, new { IngredientId = ingredientId }).SingleOrDefault();
    return ingredient;
  }

  internal void DeleteIngredient(int ingredientId)
  {
    string sql = @"
    DELETE FROM ingredients WHERE id = @IngredientId LIMIT 1;";
    int rowsAffected = _db.Execute(sql, new { ingredientId });
    if (rowsAffected == 0) throw new Exception("Delete was unsuccessful!");
    if (rowsAffected > 1) throw new Exception("Delete was too successful!");
  }

  internal List<Ingredient> GetIngredientByRecipeId(int recipeId)
  {
    string sql = @"SELECT ingredients.*, recipes.* 
    FROM ingredients 
    INNER JOIN recipes ON recipes.id = ingredients.recipe_id 
    WHERE ingredients.recipe_id = @RecipeId;";

    List<Ingredient> ingredients = _db.Query(sql, (Ingredient ingredients, Recipe recipe) =>
    {
      ingredients.RecipeId = recipe.Id;
      return ingredients;
    }, new { recipeId }).ToList();
    return ingredients;
  }
}