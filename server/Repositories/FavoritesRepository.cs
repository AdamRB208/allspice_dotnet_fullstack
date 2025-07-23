
namespace allspice_dotnet_fullstack.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites(recipe_id, account_id)
    VALUES(@RecipeId, @AccountId);
    
    SELECT favorites.*, recipes.*,
    favorites.id AS favoriteId,
    favorites.recipe_id AS recipeId,
    favorites.account_id AS accountId
    FROM favorites
    INNER JOIN recipes ON favorites.recipe_id = recipes.id
    WHERE favorites.id = LAST_INSERT_ID();";

    Favorite favorite = _db.Query<Favorite>(sql, favoriteData).SingleOrDefault();
    return favorite;
  }


  internal List<FavoriteRecipe> GetFavoriteRecipes(string accountId)
  {
    string sql = @"
    SELECT favorites.*, recipes.*, accounts.* FROM favorites
    INNER JOIN recipes ON recipes.id = favorites.recipe_id
    INNER JOIN accounts ON accounts.id = recipes.creator_id
    WHERE favorites.account_id = @accountId;";

    List<FavoriteRecipe> favorites = _db.Query(sql, (Favorite favorite, FavoriteRecipe favoriteRecipe, Profile profile) =>
    {
      favoriteRecipe.FavoriteId = favorite.Id;
      favoriteRecipe.AccountId = favorite.AccountId;
      favoriteRecipe.Creator = profile;
      return favoriteRecipe;
    }, new { accountId }).ToList();
    return favorites;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = @"
    SELECT favorites.*, accounts.* 
    FROM favorites
    INNER JOIN accounts ON favorites.account_id = accounts.id
    WHERE favorites.id = @FavoritesId;";

    Favorite foundFavorite = _db.Query(sql, (Favorite favorite, Profile account) =>
    {
      favorite.AccountId = account.Id;
      return favorite;
    }, new { FavoritesId = favoriteId }).SingleOrDefault();
    return foundFavorite;
  }

  internal void DeleteFavorite(int favoriteId)
  {
    string sql = @"
    DELETE FROM favorites WHERE favorites.id = @FavoriteId;";
    int rowsAffected = _db.Execute(sql, new { favoriteId });
    if (rowsAffected == 0) throw new Exception("Delete was unsuccessful!");
    if (rowsAffected > 1) throw new Exception("Delete was too successful!");
  }
}