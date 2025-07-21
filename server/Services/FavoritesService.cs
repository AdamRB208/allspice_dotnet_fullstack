


namespace allspice_dotnet_fullstack.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _favoritesRepository;

  public FavoritesService(FavoritesRepository favoritesRepository)
  {
    _favoritesRepository = favoritesRepository;
  }

  internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
  {
    FavoriteRecipe favorite = _favoritesRepository.CreateFavorite(favoriteData);
    return favorite;
  }


  internal List<FavoriteRecipe> GetFavoriteRecipes(string accountId)
  {
    List<FavoriteRecipe> favoriteRecipes = _favoritesRepository.GetFavoriteRecipes(accountId);
    return favoriteRecipes;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);
    if (favorite == null)
    {
      throw new Exception("Invalid favorite Id: " + favoriteId);
    }
    return favorite;
  }

  internal string DeleteFavorite(int favoriteId, Account userInfo)
  {
    Favorite favorite = GetFavoriteById(favoriteId);
    if (favorite.AccountId != userInfo.Id)
    {
      throw new Exception($"You are not allowed to delete someone elses favorite, {userInfo.Name}!");
    }
    _favoritesRepository.DeleteFavorite(favoriteId);
    return $"Deleted favorite!";
  }
}