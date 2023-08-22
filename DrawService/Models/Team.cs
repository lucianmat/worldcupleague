namespace DrawService.Models
{
   /// <summary>
   /// Team in a draw or configuration file
   /// </summary>
    public class Team
    {
        [Required]
        [JsonPropertyName("name")]
        [StringLength(50, MinimumLength = 3)]
        [Description("Name of the team")]
        public string Name { get; set; }
    }
}