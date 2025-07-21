
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
  internal string DeleteRecipe(int recipeId, Account userInfo)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot delete another users recipe, {userInfo.Name}!");
    }
    _recipesRepository.DeleteRecipe(recipeId);
    return $"Deleted recipe {recipe.Title}!";
  }

  internal Recipe UpdateRecipe(int recipeId, Recipe recipeUpdateData, Account userInfo)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"You are not allowed to update someone elses recipe, {userInfo.Name.ToUpper()}!");
    }

    recipe.Instructions = recipeUpdateData.Instructions ?? recipe.Instructions;
    recipe.Title = recipeUpdateData.Title ?? recipe.Title;
    recipe.Img = recipeUpdateData.Img ?? recipe.Img;

    _recipesRepository.UpdateRecipe(recipe);
    return recipe;
  }

  internal List<Recipe> GetUsersRecipes(string accountId)
  {
    List<Recipe> recipes = _recipesRepository.GetUsersRecipes(accountId);
    return recipes;
  }
}