
namespace allspice_dotnet_fullstack.Services;

public class RecipesService
{
  private readonly RecipesRepository _recipesRepository;

  public RecipesService(RecipesRepository recipesRepository)
  {
    _recipesRepository = recipesRepository;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _recipesRepository.CreateRecipe(recipeData);
    return recipe;
  }
}