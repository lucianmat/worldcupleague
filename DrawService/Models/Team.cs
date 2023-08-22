namespace DrawService.Models
{
    public class Team
    {
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}