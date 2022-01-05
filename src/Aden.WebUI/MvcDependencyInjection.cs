using System.Text.Json.Serialization;
using Aden.WebUI.Filters;
using DateOnlyTimeOnly.AspNet.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Aden.WebUI;

public static class MvcDependencyInjection
{
    public static IServiceCollection AddMvcDependencyInjection(this IServiceCollection services)
    {
        services.AddRazorPages();
        services
            .AddControllers(options => { options.Filters.Add<ApiExceptionFilterAttribute>(); })
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.InvalidModelStateResponseFactory = ModelStateValidator.ValidateModelState;
            });

        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.UseApiBehavior = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader(), //defaults to "api-version"
                new QueryStringApiVersionReader("v"),
                new HeaderApiVersionReader("api-version"),
                new HeaderApiVersionReader("v"),
                new MediaTypeApiVersionReader(), //defaults to "v"
                new MediaTypeApiVersionReader("api-version"));
        });

        services.AddVersionedApiExplorer(
            options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";
                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
        
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        
        return services;
    }
}

public class ModelStateValidator
{
    public static IActionResult ValidateModelState(ActionContext context)
    {
        (string fieldName, ModelStateEntry entry) = context.ModelState
            .First(x => x.Value.Errors.Count > 0);
        string errorSerialized = entry.Errors.First().ErrorMessage;

        // Error error = Error.Deserialize(errorSerialized);
        // Envelope envelope = Envelope.Error(error, fieldName);
        // var result = new BadRequestObjectResult(envelope);
        var result = new BadRequestObjectResult("Custom Error");

        return result;
    }
}