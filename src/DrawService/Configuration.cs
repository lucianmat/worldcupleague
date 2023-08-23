using Microsoft.Extensions.Configuration;

namespace DrawService;

public static class ConfigurationManager {
    public static string? ConnectionString { get; set; } 

    public static void Initialize(string configFile="drawsettings.json")
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
                                                .AddJsonFile(configFile, false, false);

        IConfigurationRoot root = builder.Build();

        ConnectionString = root.GetConnectionString("DrawDb");
    }
        
    
}