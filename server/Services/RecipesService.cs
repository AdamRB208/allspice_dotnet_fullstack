



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

  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _recipesRepository.GetAllRecipes();
    return recipes;
  }

  internal List<Recipe> GetAllRecipes(string category)
  {
    List<Recipe> recipes = _recipesRepository.GetAllRecipesByQuery(category);
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _recipesRepository.GetRecipeById(recipeId);
    if (recipe == null)
    {
      throw new Exception("Invalid recipe Id: " + recipeId);
    }
    return recipe;
  }
}