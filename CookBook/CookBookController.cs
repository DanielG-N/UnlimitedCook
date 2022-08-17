using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[ApiController]
[Route("cookbook")]
public class CookBookController : ControllerBase
{
    private readonly IMongoCollection<CookBook> _cookBookDB;

    public CookBookController(CookBookDB cookBookDB) =>
        _cookBookDB = cookBookDB._cookBookCollection;

    [HttpGet]
    public async Task<ActionResult<List<CookBook>>> GetAllCookBooks()
    {
        return await _cookBookDB.Find(_ => true).ToListAsync();
    }

    [HttpPost]
    public async Task<IResult> CreateCookBook(CookBook cookBook)
    {
        await _cookBookDB.InsertOneAsync(cookBook);
        return Results.Created($"/{cookBook.Id}", cookBook);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CookBook>> GetCookBook(int id)
    {
        var cookBook = await _cookBookDB.Find(x => x.Id == id).FirstOrDefaultAsync();

        if(cookBook == null)
            return NotFound();

        return Ok(cookBook);
    }

    [HttpPut("{cookBookId}")]
    public async Task<IResult> AddRecipe([FromBody]string recipeId, [FromQuery]int cookBookId)
    {
        var cookBook = await _cookBookDB.Find(x => x.Id == cookBookId).FirstOrDefaultAsync();
        cookBook.RecipeIds.Add(recipeId);

        await _cookBookDB.FindOneAndReplaceAsync(r => r.Id == cookBookId, cookBook);
        return Results.Created($"/{cookBook.Id}", cookBook);
    }

    [HttpPut("remove/{cookBookId}")]
    public async Task<IResult> RemoveRecipe([FromBody]string recipeId, [FromQuery]int cookBookId)
    {
        var cookBook = await _cookBookDB.Find(x => x.Id == cookBookId).FirstOrDefaultAsync();
        cookBook.RecipeIds.Remove(recipeId);

        await _cookBookDB.FindOneAndReplaceAsync(r => r.Id == cookBookId, cookBook);
        return Results.Created($"/{cookBook.Id}", cookBook);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteRecipe(int id)
    {
        var cookBook = await _cookBookDB.FindOneAndDeleteAsync(r => r.Id == id);

        return Results.Ok(cookBook);
    }
}