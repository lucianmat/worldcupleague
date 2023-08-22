
/// <summary>
/// Configure the API versioning middleware
/// </summary>
public static class ApiVersionExtension {

    /// <summary>
    /// Configure the API versioning middleware
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
     public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;

                // Set a default version when it's not provided,
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);

                // Combine (or not) API Versioning Mechanisms:
                options.ApiVersionReader = ApiVersionReader.Combine(
                        new QueryStringApiVersionReader("api-version"),
                        new HeaderApiVersionReader("Accept-Version"),
                        new MediaTypeApiVersionReader("api-version")
                    );
            })
            .AddApiExplorer(options =>
            {
                // Format the version as "v{Major}.{Minor}.{Patch}" (e.g. v1.0.0).
                options.GroupNameFormat = "'v'VVV";

                options.SubstituteApiVersionInUrl = true;
            });

            return services;
    }
}