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

    [HttpPost]
    public async Task<IResult> AddRecipe(Recipe recipe)
    {
        await _recipeDB.InsertOneAsync(recipe);
        return Results.Created($"/{recipe.Id}", recipe);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetItem(string id)
    {
        var recipe = await _recipeDB.Find(x => x.Id == id).FirstOrDefaultAsync();

        if(recipe == null)
            return NotFound();

        return Ok(recipe);
    }

    [HttpPut]
    public async Task<IResult> UpdateRecipe(Recipe recipe)
    {
        await _recipeDB.FindOneAndReplaceAsync(r => r.Id == recipe.Id, recipe);
        return Results.Created($"/{recipe.Id}", recipe);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteRecipe(string id)
    {
        var recipe = await _recipeDB.FindOneAndDeleteAsync(r => r.Id == id);

        return Results.Ok(recipe);
    }
}