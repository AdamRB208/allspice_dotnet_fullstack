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
}