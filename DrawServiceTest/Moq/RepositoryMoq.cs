
public static class RepositoryMoq {

    /// <summary>
    /// Creates a mock repository with the given countries
    /// </summary>
    /// <param name="countries"></param>
    /// <returns></returns>
    public static Mock<IDrawRepository> GetMock(IEnumerable<Country>? countries = null) {
        var drawServiceRepositoryMock = new Mock<IDrawRepository>();

        drawServiceRepositoryMock.Setup(x => x.GetAllTeams())
            .Returns(countries ?? new List<Country>());

        drawServiceRepositoryMock.
            Setup(x => x.SaveDraw(It.IsAny<DrawRequest>(), It.IsAny<IEnumerable<Group>>()))
            .Verifiable();

        return drawServiceRepositoryMock;
    }

    /// <summary>
    /// Reads a file with the following format: 
    ///     Country1:Team1,Team2,Team3
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static Mock<IDrawRepository> GetMockFromFile(string filename) {
        
        if (!File.Exists(filename)) 
            throw new FileNotFoundException(filename);

        var textLines = File.ReadAllText(filename).Split("\n")
            .Where(p=>!string.IsNullOrWhiteSpace(p.Trim()));

        var list = textLines.Aggregate(new List<Country>(), (lst, text)=>{
            string[] parts = text.Split(":");

            if (parts.Length == 2) {  
                lst.Add(new Country{ 
                                Name = parts[0].Trim(), 
                                Teams = parts[1].Split(",")
                                            .Select(d=>new Team(){Name=d.Trim()}).ToList() 
                                });
            }

            return lst;
            });

        return GetMock(list);
    }

    /// <summary>
    /// Creates a mock repository with random data
    /// </summary>
    /// <param name="countries"></param>
    /// <param name="teams"></param>
    /// <returns></returns>
    public static Mock<IDrawRepository> GetRandomMockData(int countries=8, int teams=4){
        var list = new List<Country>();
        
        for(var vi=1; vi<=countries; vi++){
            var teamList = new List<Team>();

            for(var ti=1; ti<=teams; ti++){
                teamList.Add(new Team(){ Name = $"Team{vi}-{ti}" });
            }
            var country = new Country(){ Name = $"Country{vi}", Teams = teamList };
            
            list.Add(country);
        }
        return GetMock(list);
    }
}