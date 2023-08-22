namespace DrawService.Models
{
    /// <summary>
    /// Group of teams in a draw
    /// </summary>
    public class Group
    {
        [JsonPropertyName("groupName")]
        [Required]
        [Description("Name of the group")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [JsonPropertyName("teams")]
        [Required]
        public IEnumerable<Team> Teams { get; set; }
    }
}