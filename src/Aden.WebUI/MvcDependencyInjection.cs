using System.Text.Json.Serialization;
using Aden.Application.Submission.Commands;
using Aden.WebUI.Filters;
using DateOnlyTimeOnly.AspNet.Converters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Aden.WebUI;

public static class MvcDependencyInjection
{
    public static IServiceCollection AddMvcDependencyInjection(this IServiceCollection services)
    {
        services
            .AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
            .AddFluentValidation(x =>
                x.RegisterValidatorsFromAssemblyContaining<CreateSubmission.Command>()
            )
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                }
            ).ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddRazorPages();

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
