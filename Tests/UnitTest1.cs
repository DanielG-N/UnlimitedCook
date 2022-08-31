using static Recipe;
using static User;
using static CookBook;
using Moq;
using MongoDB.Driver;

namespace Tests;

public class UnitTest1
{
    private readonly Mock<RecipeDB> _recipeMockRepo;
    private readonly RecipeController _recipeController;

    private readonly Mock<CookBookDB> _cookbookMockRepo;
    private readonly CookBookController _cookbookController;



    public UnitTest1()
    {
        _recipeMockRepo = new Mock<RecipeDB>();
        _recipeMockRepo.Object._recipeCollection = new Mock<IMongoCollection<Recipe>>().Object;
        _recipeController = new RecipeController(_recipeMockRepo.Object);

        _cookbookMockRepo = new Mock<CookBookDB>();
        _cookbookMockRepo.Object._cookBookCollection = new Mock<IMongoCollection<CookBook>>().Object;
        _cookbookController = new CookBookController(_cookbookMockRepo.Object);
    }

    [Fact]
    public void RecipeAddTest()
    {
        var recipe = new Recipe { RecipeName = "water", Ingredients = new List<string>{"water"}, Instructions = new List<string>{"pour water in cup"}};
        var result = _recipeController.AddRecipe(recipe);

        Assert.True(result.IsCompletedSuccessfully);
    }

    // [Fact]
    // public void RecipeGetAllTest()
    // {
    //     var recipe = new Recipe { RecipeName = "water", Ingredients = new List<string>{"water"}, Instructions = new List<string>{"pour water in cup"}};
    //     //var asyncCursor = new Mock<IAsyncCursor<Recipe>>();
    //     // _recipeMockRepo.Setup(x => x._recipeCollection.Find(_ => true,
    //     //     It.IsAny<FindOptions<Recipe>>(),
    //     //     default));
    //     //_recipeMockRepo.Setup(p => p._recipeCollection.Find(_ => true, It.IsAny<FindOptions>())).Returns(() => new List<Recipe>{new Recipe()});
    //     var r = _recipeController.AddRecipe(recipe);

    //     // asyncCursor.SetupSequence(_async => _async.MoveNext(default)).Returns(true).Returns(false);
    //     // asyncCursor.SetupGet(_async => _async.Current).Returns(new List<Recipe>());

    //     var result = _recipeController.GetAllRecipes();

    //     //Console.WriteLine(result.ToString());

    //     Assert.True(result.IsCompletedSuccessfully);
    // }

    [Fact]
    public void RecipeDeleteTest()
    {
        string id = "yo";
        var recipe = new Recipe {Id = "yo", RecipeName = "water", Ingredients = new List<string>{"water"}, Instructions = new List<string>{"pour water in cup"}};

        var r = _recipeController.AddRecipe(recipe);

        var result = _recipeController.DeleteRecipe(id);


        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public void RecipeUpdateTest()
    {
        var recipe = new Recipe {Id = "yo", RecipeName = "water", Ingredients = new List<string>{"water"}, Instructions = new List<string>{"pour water in cup"}};

        var r = _recipeController.AddRecipe(recipe);

        recipe.RecipeName = "water";

        var result = _recipeController.UpdateRecipe(recipe);


        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public void CookBookAddTest()
    {
        var cookbook = new CookBook { Id = 1, RecipeIds = new List<string>()};
        var result = _cookbookController.CreateCookBook(cookbook);

        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public void CookBookDeleteTest()
    {
        int id = 1;
        var cookbook = new CookBook { Id = 1, RecipeIds = new List<string>()};

        var r = _cookbookController.CreateCookBook(cookbook);

        var result = _cookbookController.DeleteCookBook(id);


        Assert.True(result.IsCompletedSuccessfully);
    }

    // [Fact]
    // public void CookBookAddRecipeTest()
    // {
    //     var cookbook = new CookBook { Id = 1, RecipeIds = new List<string>()};
    //     _cookbookMockRepo.Setup(x => x._cookBookCollection.FindAsync(r => r.Id == 1,
    //         It.IsAny<FindOptions<CookBook, CookBook>>(), default)).Returns(() => cookbook);

    //     var r = _cookbookController.CreateCookBook(cookbook);
    //     var result = _cookbookController.AddRecipe("yo", 1);

    //     Assert.True(result.IsCompletedSuccessfully);
    // }
}