

namespace allspice_dotnet_fullstack.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites(id, created_at, updated_at, account_id, recipe_id)
    VALUES(@Id, @CreatedAt, @UpdatedAt, @AccountId, @RecipeId);
    
    SELECT favorites.*, recipes.*,
    favorites.id AS favoriteId
    FROM favorites
    INNER JOIN recipes ON favorites.recipe_id = recipes.id
    WHERE favorites.id = LAST_INSERT_ID();";

    FavoriteRecipe favorite = _db.Query<FavoriteRecipe>(sql, favoriteData).SingleOrDefault();
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
}