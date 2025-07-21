namespace allspice_dotnet_fullstack.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _favoritesRepository;

  public FavoritesService(RavoritesRepository favoritesRepository)
  {
    _favoritesRepository = favoritesRepository;
  }

  
}