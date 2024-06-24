using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bookify.Api.OpenApi
{
    public sealed class ConfigureSwaggerOptions(
        IApiVersionDescriptionProvider apiVersionDescriptionProvider) 
        : IConfigureNamedOptions<SwaggerGenOptions> 
    {
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription apiVersionDescription)
        {
            var openApiInfo = new OpenApiInfo()
            {
                Title = $"Bookify.Api v{apiVersionDescription.ApiVersion}",
                Version = apiVersionDescription.ApiVersion.ToString()
            };

            if (apiVersionDescription.IsDeprecated)
            {
                openApiInfo.Description += "This API version has been deprecated.";
            }

            return openApiInfo;

        }
    }
}
