using System.ComponentModel.DataAnnotations;

namespace Server.Controllers;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0", Deprecated = false)]
[ApiVersion("2.0", Deprecated = false)] // - test for versioning
public class DrawController : ControllerBase
{

    private readonly ILogger<DrawController> _logger;

    public DrawController(ILogger<DrawController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get all countries and teams associated
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllCountries")]
    [MapToApiVersion("2.0")]
    public IEnumerable<Country>? GetAllCountriesV2()
    {
        return new List<Country>();
     }


    /// <summary>
    /// Execute random draw
    /// </summary>
    /// <param name="request">user who execute draw</param>
    /// <returns></returns>
    [HttpPost("DrawCountries")]
     public IEnumerable<Group>? Draw([FromBody, Required] DrawRequest request){
        return null;
     }
}