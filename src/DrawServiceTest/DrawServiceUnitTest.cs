namespace DrawServiceTest;

[TestFixture(Category ="DrawService")]
public class DrawServiceTest
{
  
    [Test(Description = "Test that the draw service returns the correct number of teams")]
    public void TestInitialize()
    {
        var repository = RepositoryMoq.GetRandomMockData(8,4);

        var drawService = new DrawService.DrawService(repository.Object);
    
        var teams = drawService.GetAllTeams();

        // check that the repository function was called
        repository.Verify(x => x.GetAllTeams(), Times.Once);

        Assert.IsNotNull(teams);
        // verify teams are 8
        Assert.AreEqual(teams!.Count(), 8);

        // verify each country has 4 teams
        Assert.IsTrue(teams!.Aggregate(true, (acc, country) => acc && country.Teams.Count() == 4));
    }

    [Test(Description = "Test that the draw service returns the groups as requested")]
    public void TestDraw()
    {
        var repository = RepositoryMoq.GetRandomMockData();

        var drawService = new DrawService.DrawService(repository.Object);
    
        var groups = drawService.Draw(new DrawRequest() { UserName="user", NumberOfGroups = 8 });

        repository.Verify(x => x.GetAllTeams(), Times.Once);

        Assert.IsNotNull(groups);
    }
}