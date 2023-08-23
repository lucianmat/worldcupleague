
namespace DrawService.Interfaces;

/// <summary>
/// Interface for DrawService implementation
/// </summary>
public interface IDrawService {

    /// <summary>
    /// Execute random draw
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    IEnumerable<Group>? Draw(DrawRequest request);

    /// <summary>
    /// Get all teams from repository
    /// </summary>
    /// <returns></returns>
    IEnumerable<Country>? GetAllTeams();
}