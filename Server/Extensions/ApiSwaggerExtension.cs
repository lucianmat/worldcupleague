using System.Reflection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class ApiSwaggerExtension {
        /// <summary>
        /// Configure the Swagger generator with XML comments, bearer authentication, etc.
        /// Additional configuration files:
        /// <list type="bullet">
        ///     <item>ApiSwaggerGenOptions.cs</item>
        /// </list>
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureApiSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ApiSwaggerGenOptions>();
      services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);              
            });
        }
}