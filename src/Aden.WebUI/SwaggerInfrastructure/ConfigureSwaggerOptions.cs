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
            //options.CustomSchemaIds(type => type.ToString());
            //options.CustomSchemaIds(type => $"{type.Namespace}_{type.Name}_{Guid.NewGuid()}");
            //options.CustomSchemaIds(type => $"{Guid.NewGuid()}_{type.Namespace}_{type.FullName}");
            options.CustomSchemaIds(x => $"{x.FullName}_{Guid.NewGuid()}");
            options.SchemaFilter<NamespaceSchemaFilter>();
            
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
    
    public class NamespaceSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema is null)
            {
                throw new System.ArgumentNullException(nameof(schema));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            schema.Title = context.Type.Name; // To replace the full name with namespace with the class name only
        }
    }
}
