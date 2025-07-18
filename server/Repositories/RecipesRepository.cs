namespace allspice_dotnet_fullstack.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  
}