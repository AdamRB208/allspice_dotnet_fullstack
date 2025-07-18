namespace allspice_dotnet_fullstack.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly RecipesService _recipeService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, RecipesService recipeService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _recipeService = recipeService;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpGet("recipes")]
  public async Task<ActionResult<List<Recipe>>> GetUsersRecipes()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Recipe> recipes = _recipeService.GetUsersRecipes(userInfo.Id);
      return Ok(recipes);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}
