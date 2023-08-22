using System.Collections.Generic;

namespace DrawService.Models;

/// <summary>
/// Country in a draw or configuration file
/// </summary>
public class Country {

    [Description("Name of the country")]
    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }
 
    [Description("Teams in the country")]
    [Required]
    [JsonPropertyName("teams")]
    public IEnumerable<Team> Teams { get; set; }
}