using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[ApiController]
[Route("recipe")]
public class RecipeController : ControllerBase
{
    private readonly RecipeDB _recipeDB;

    public RecipeController(RecipeDB recipeDB) =>
        _recipeDB = recipeDB;

    [HttpGet]
        public async Task<ActionResult<List<Recipe>>> GetAllItems()
        {
            return await _recipeDB._recipeCollection.Find(_ => true).ToListAsync();
        }
}