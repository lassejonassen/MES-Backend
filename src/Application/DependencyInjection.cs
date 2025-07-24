using Microsoft.AspNetCore.Builder;
using Wolverine;

namespace Application;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
    {
        builder.UseWolverine(opts =>
        {
            opts.ApplicationAssembly = typeof(DependencyInjection).Assembly;
        });

        return builder;
    }
}
