
namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0", Deprecated = false)]
// [ApiVersion("2.0", Deprecated = false)] - test for versioning
public class DrawController : ControllerBase
{

    private readonly ILogger<DrawController> _logger;

    public DrawController(ILogger<DrawController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get all countries
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllCountries", Name = "GetAllCountries")]
    public IEnumerable<Country[]>? GetAllCountries()
    {
        return null;
     }
}