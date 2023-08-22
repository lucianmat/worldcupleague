namespace DrawService.Models
{
    public class Group
    {
        [JsonPropertyName("groupName")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("teams")]
        [Required]
        public IEnumerable<Team> Teams { get; set; }
    }
}