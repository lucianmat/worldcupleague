using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace DrawService.Entities;

/// <summary>
/// This is a implementation of IDrawRepository
/// </summary>
public class DbDrawRepository : DbContext, IDrawRepository  {

    private readonly string _connectionString;

    public DbSet<DbCountry> Countries { get; set; }
    public DbSet<DbTeam> Teams { get; set; }

    public DbSet<DbResults> Results { get; set; }

    public DbDrawRepository(string connectionString) => _connectionString = connectionString;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (string.IsNullOrEmpty(_connectionString))
            throw new InvalidOperationException("Connection string for DrawDb is null or empty");

        optionsBuilder.UseSqlServer(_connectionString);
    }

    /// <summary>
    /// This method returns all the teams in the database
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Country> GetAllTeams() {

        return Countries!.Include(x => x.Teams)
                        .Select(x => new Country{ 
                            Name= x.Name, 
                            Teams= x.Teams!.Select(y => new Team{ Name = y.Name}).ToList()
                            });
    }

    public void SaveDraw(DrawRequest request, IEnumerable<Group> groups) {

        var results = new DbResults() 
            { 
                DrawRequest = JsonSerializer.Serialize(request), 
                Results = JsonSerializer.Serialize(groups)
            };

        Results!.Add(results);
        SaveChanges();
    }
}


[Table("Countries")]
public class DbCountry {
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
   public required string Name { get; set; }

    public IEnumerable<DbTeam>? Teams { get; set; }
}

[Table("Teams")]
public class DbTeam {
    [Required]
    [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public required string Name { get; set; }

    [ForeignKey("CountryId")]
    public DbCountry? Country { get; set; }
}

[Table("DrawResults")]
public class DbResults {
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public required string DrawRequest { get; set; }

   [Required]
    public required string Results { get; set; }
}