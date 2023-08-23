
// Server=localhost;Database=DrawDb_LM;User Id=sa;Password=myPassword;

using System.Reflection;

namespace DrawServiceTest;

[TestFixture(Category ="DrawRepository")]
public class DrawRepositoryTest
{

    [SetUp]
    public void Setup() {
        ConfigurationManager.Initialize();
    }

    [Test(Description = "Test that repository and database exists")]
    public void TestDatabaseCreated()
    {
        var repository = new DbDrawRepository(ConfigurationManager.ConnectionString!);
        Assert.IsNotNull(repository);

        repository.Database.EnsureCreated();
       
    }


    [Test(Description = "Test that repository and teams records exists")]
    public void TestTeamsCreated()
    {
        var repository = new DbDrawRepository(ConfigurationManager.ConnectionString!);
        Assert.IsNotNull(repository);

        var teams = repository.GetAllTeams();
        Assert.IsNotNull(teams);
        Assert.IsTrue(teams.Count() > 0);

    }

    [Test(Description = "Test that repository and draw operation succeedes")]
    public void TestDrawFromDatabase() 
    {
        var repository = new DbDrawRepository(ConfigurationManager.ConnectionString!);
        Assert.IsNotNull(repository);

        var drawService = new DrawService.DrawService(repository);
        
        var drawCount = repository.Results!.Count();

        var groups = drawService.Draw(new DrawRequest() { UserName="user", NumberOfGroups = 8 });

        Assert.IsNotNull(groups);

        // verify that the draw was saved
        Assert.AreEqual(drawCount+1, repository.Results!.Count());

    }
}