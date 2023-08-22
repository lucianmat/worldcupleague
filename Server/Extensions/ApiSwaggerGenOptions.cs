using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Asp.Versioning.ApiExplorer;

  public class ApiSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSwaggerGenOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ApiSwaggerGenOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // Add a swagger document for each discovered API version.
            // Note: you might choose to skip or document deprecated API versions differently.
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        /// <summary>
        /// Create info for API version
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Version = description.ApiVersion.ToString(),
                Title = "Draw API",
                Description = "Draw API server"
            };

            if (description.IsDeprecated)
            {
                info.Description += " [This API version has been deprecated]";
            }

            return info;
        }
    }