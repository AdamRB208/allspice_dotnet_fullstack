namespace allspice_dotnet_fullstack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly RecipesService _recipeService;
  private readonly Auth0Provider _auth0Provider;

  public RecipesController(RecipesService recipeService, Auth0Provider auth0Provider)
  {
    _recipeService = recipeService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      Recipe recipe = _recipeService.CreateRecipe(recipeData);
      return Ok(recipe);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Recipe>> GetAllRecipes([FromQuery] string category)
  {
    try
    {
      List<Recipe> recipes;
      if (category == null)
      {
        recipes = _recipeService.GetAllRecipes();
      }
      else
      {
        recipes = _recipeService.GetAllRecipes(category);
      }
      return Ok(recipes);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipeService.GetRecipeById(recipeId);
      return Ok(recipe);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [Authorize]
  [HttpDelete("{recipeId}")]
  public async Task<ActionResult<string>> DeleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _recipeService.DeleteRecipe(recipeId, userInfo);
      return Ok(message);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [Authorize]
  [HttpPut("{recipeId}")]
  public async Task<ActionResult<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe recipeUpdateData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipeService.UpdateRecipe(recipeId, recipeUpdateData, userInfo);
      return Ok(recipe);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}