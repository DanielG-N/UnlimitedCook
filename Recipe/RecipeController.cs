using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq; 

[ApiController]
[Route("recipe")]
public class RecipeController : ControllerBase
{
    private readonly IMongoCollection<Recipe> _recipeDB;

    public RecipeController(RecipeDB recipeDB) =>
        _recipeDB = recipeDB._recipeCollection;

    [HttpPost]
    public async Task<IResult> AddRecipe(Recipe recipe)
    {
        await _recipeDB.InsertOneAsync(recipe);
        return Results.Created($"/{recipe.Id}", recipe);
    }

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> GetAllRecipes()
    {
        return await _recipeDB.Find(_ => true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetRecipe(string id)
    {
        var recipe = await _recipeDB.Find(x => x.Id == id).FirstOrDefaultAsync();

        if(recipe == null)
            return NotFound();

        return Ok(recipe);
    }

    [HttpGet("getRecipes")]
    public async Task<ActionResult<List<Recipe>>> GetRecipesFromList(List<string> ids)
    {
        return await _recipeDB.Find(Builders<Recipe>.Filter.In(r => r.Id, ids)).ToListAsync();
    }

    [HttpGet("randomRecipes")]
    public async Task<ActionResult<List<Recipe>>> GetRandomRecipes()
    {
        return await _recipeDB.AsQueryable().Sample(10).ToListAsync();
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