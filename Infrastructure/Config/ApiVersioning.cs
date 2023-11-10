using Microsoft.AspNetCore.Mvc;

namespace LearnApi.Infrastructure.Config
{
    public class ApiVersioning
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(
                o =>
                {
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                }
            );

            builder.Services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
        }
    }
}