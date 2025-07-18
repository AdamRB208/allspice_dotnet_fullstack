
namespace allspice_dotnet_fullstack.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO
    recipes(id, created_at, updated_at, title, instructions, img, category, creator_id)
    VALUES(@Id, @CreatedAt, @UpdatedAt, @Title, @Instructions, @Img, @Category, @CreatorId);
    
    SELECT recipes.*, accounts.* 
    FROM recipes 
    INNER JOIN accounts ON recipes.creator_id = accounts.id 
    WHERE recipes.id = LAST_INSERT_ID();";

    Recipe createdRecipe = _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, recipeData).SingleOrDefault();
    return createdRecipe;
  }
}