

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
}