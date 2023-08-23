namespace DrawService.Interfaces;

public interface IDrawRepository {

    /// <summary>
    /// Get all teams from repository
    /// </summary>
    /// <returns></returns>
    IEnumerable<Country> GetAllTeams();

    /// <summary>
    /// Save draw result to repository
    /// </summary>
    /// <param name="request"></param>
    /// <param name="groups"></param>
    void SaveDraw(DrawRequest request, IEnumerable<Group> groups);

}