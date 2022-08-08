using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[ApiController]
[Route("recipe")]
public class RecipeController : ControllerBase
{
    private readonly IMongoCollection<Recipe> _recipeDB;

    public RecipeController(RecipeDB recipeDB) =>
        _recipeDB = recipeDB._recipeCollection;

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> GetAllRecipes()
    {
        return await _recipeDB.Find(_ => true).ToListAsync();
    }
}