using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Aden.WebUI.SwaggerInfrastructure
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
            => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Students API",
                Version = description.ApiVersion.ToString(),
                Description = "API for working with Students.",
                Contact = new OpenApiContact() { Name = "Mark Lawrence", Email = "mlawrence@alsde.edu" },
                //TermsOfService = new System.Uri("https://www.linktotermsofservice.com"),
                //License = new OpenApiLicense()
                //{ Name = "MIT", Url = new System.Uri("https://opensource.org/licenses/MIT") }
            };
            if (description.IsDeprecated)
            {
                info.Description += "<span style=\"color:red\"> This API version has been deprecated.</span>";
            }

            return info;
        }
    }
}
