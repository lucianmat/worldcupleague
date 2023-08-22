using System.Collections.Generic;

namespace DrawService.Models;

public class Country {

    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }
 
    public IEnumerable<Team> Teams { get; set; }
}