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

  
}