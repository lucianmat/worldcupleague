namespace DrawService;

public class DrawService : IDrawService
{

 public IEnumerable<Models.Country> GetAllTeams()
 {
  return new List<Models.Country>() { new Models.Country() { Name = "Germany", Teams = new List<Team>() { new Team {  Name = "Berlin" } }} };
 }
}
