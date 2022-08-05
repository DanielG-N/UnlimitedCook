using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("recipe")]
public class RecipeController : ControllerBase
{
    private readonly RecipeDB _recipeDB;

    public RecipeController(RecipeDB recipeDB) =>
        _recipeDB = recipeDB;
}