using static Recipe;
using static User;
using Moq;
namespace Tests;

public class UnitTest1
{
    private readonly Mock<RecipeDB> _recipeMockRepo;
    private readonly RecipeController _recipeController;

    public UnitTest1()
    {
        _recipeMockRepo = new Mock<RecipeDB>();
        _recipeController = new RecipeController(_recipeMockRepo.Object);
    }

    [Fact]
    public void RecipeAddTest()
    {
        
    }
}