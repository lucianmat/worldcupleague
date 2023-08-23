using System.ComponentModel.DataAnnotations;

namespace Server.Controllers;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("api/[controller]")]
//[Route("api/v{version:apiVersion}/[controller]")]
//[ApiVersionNeutral]
[ApiVersion("1.0", Deprecated = false)]
[ApiVersion("2.0", Deprecated = false)] // - test for versioning
public class DrawController : ControllerBase
{

    private readonly ILogger<DrawController> _logger;

    /// <summary>
    /// This is the service that will be used to execute the draw
    /// </summary>
    private readonly IDrawService _service;

    public DrawController(ILogger<DrawController> logger, IDrawService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// Get all countries and teams associated
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllCountries")]
    [MapToApiVersion("2.0")]
    public IEnumerable<Country>? GetAllCountriesV2()
    {
        _logger.LogDebug("GetAllCountriesV2");
        return _service.GetAllTeams();
     }


    /// <summary>
    /// Execute random draw
    /// </summary>
    /// <param name="request">user who execute draw</param>
    /// <returns></returns>
    [HttpPost("DrawCountries")]
  //  [MapToApiVersion("2.0")]
  //  [MapToApiVersion("1.0")]
     public IEnumerable<Group>? Draw([FromBody, Required] DrawRequest request)
     {
        _logger.LogDebug("Draw");
        return _service.Draw(request);
     }
}