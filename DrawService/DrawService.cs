namespace DrawService;

/// <summary>
/// Draw service implementation
/// </summary>
/// <seealso cref="IDrawService"/>
public class DrawService : IDrawService
{
    readonly IDrawRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawService"/> class.
    /// </summary>
    /// <param name="repository">The <see cref="IDrawRepository">repository</see> used to generate Swagger documents.</param>
    public DrawService(IDrawRepository repository) => this.repository = repository;


    /// <summary>
    /// Draws the specified request - implementation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="InvalidDataException">If there are no countries to draw from</exception>
    public  IEnumerable<Group>? Draw(DrawRequest request) {
        var countries = repository.GetAllTeams();
      
        if (countries == null || countries.Count() < request.NumberOfGroups) 
            throw new InvalidDataException("Not enough countries to draw");
        
        // normalize 
        var teamlist = countries.SelectMany(a=> a.Teams, 
                        (a, t)=> new {Country = a.Name,Team= t.Name})
                        .ToList();

        Dictionary<string,Dictionary<string,string>> result = new();
        
        int groupNmbr = 0;
        Random rng = new(); // used to shuffle the teams

        bool itemsAvailable = false;
        Dictionary<string,string> allocatedTeams = new();

        do {
            string groupKey = ((char)('A' + groupNmbr)).ToString();
            
            // create group if not exists
            if (!result.ContainsKey(groupKey)) {
                result.Add(groupKey, new());
            }
            
            // get all countries already in the group
            var gcountries = result[groupKey].Keys.ToList();

            // get all teams that have countries which are not in the group and not already alocated
            var available = teamlist.Where(x => !gcountries.Contains(x.Country)
                        && !allocatedTeams.ContainsKey(x.Country+ ":" +x.Team))
                            .OrderBy(d=>rng.Next()) // on random order
                            .ToList();

            itemsAvailable = available.Count > 0;
            
            if (!itemsAvailable) break; // no more teams available

            var team = available[0];
         
            result[groupKey].Add(team.Country, team.Team);

            allocatedTeams.Add(team.Country+ ":" +team.Team, groupKey);
            
            groupNmbr++; // go to next group
            if (groupNmbr >= request.NumberOfGroups) groupNmbr = 0;

        } while(itemsAvailable); // not really used, but just in case

        var groups = result.Select(x => new Group() { 
                Name = x.Key, 
                Teams = x.Value.Select(d=>new Team () { Name= d.Value}) })
                .AsEnumerable();

        repository.SaveDraw(request, groups);

        return groups;
    }

        /// <summary>
        /// Gets all teams from repository
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Country>? GetAllTeams()
        {
            return  repository?.GetAllTeams();
        }
}
