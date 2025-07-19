


namespace allspice_dotnet_fullstack.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _ingredientsRepository;
  private readonly RecipesService _recipesService;

  public IngredientsService(IngredientsRepository ingredientsRepository, RecipesService recipesService)
  {
    _ingredientsRepository = ingredientsRepository;
    _recipesService = recipesService;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    Ingredient ingredient = _ingredientsRepository.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception("Invalid ingredient Id: " + ingredientId);
    }
    return ingredient;
  }

  private void CheckRecipeOwnership(Ingredient ingredient, Account userInfo)
  {
    Recipe recipe = _recipesService.GetRecipeById(ingredient.RecipeId);
    if (userInfo.Id != recipe.CreatorId)
    {
      throw new Exception("You are unable to make changes to a recipe you do not own!");
    }
  }

  internal string DeleteIngredient(int ingredientId, Account userInfo)
  {
    Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);
    CheckRecipeOwnership(ingredient, userInfo);
    _ingredientsRepository.DeleteIngredient(ingredientId);
    return "Ingredient has been Deleted!";
  }

  internal List<Ingredient> GetIngredientByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _ingredientsRepository.GetIngredientByRecipeId(recipeId);
    return ingredients;
  }
}