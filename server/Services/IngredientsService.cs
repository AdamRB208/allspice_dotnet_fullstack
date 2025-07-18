
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
}