using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseDefaults(this WebApplication app, bool useNswag = false)
    {
        //bool useAuthentication = app.Configuration.GetValue<bool>("Authentication:Enabled") || app.Configuration.GetValue<bool>("AppSettings:Authentication:Enabled");

        //if (useNswag)
        //{
        //    app.UseOpenApi();
        //}
        //else
        //{
        //    app.UseSwagger();
        //}

        //app.UseSwaggerUI();

        //app.UseMiddleware<CorrelationIdMiddleware>();
        //app.UseMiddleware<TraceIdMiddleware>();

        //app.UseSerilogRequestLogging();

        app.UseExceptionHandler();

        app.UseCors(x =>
        {
            x.AllowAnyHeader();
            x.AllowAnyMethod();
            x.AllowAnyOrigin();
        });

        //if (useAuthentication)
        //{
        //    app.UseAuthentication();
        //    app.UseAuthorization();
        //    app.MapControllers()
        //        .RequireAuthorization();
        //}
        //else
        //{
            app.UseAuthorization();
            app.MapControllers();
        //}

        app.MapGet("/", () => { return "Always On"; }); // used by Azure App Service

        return app;
    }

    public static WebApplication UseDefaults<TDbContext>(this WebApplication app) where TDbContext : DbContext
    {
        if (app.Environment.IsDevelopment())
        {
            app.ApplyMigrations<TDbContext>();
        }

        UseDefaults(app);

        return app;
    }

    public static WebApplication UseDefaults<TDbContext>(this WebApplication app, bool useNswag = false) where TDbContext : DbContext
    {
        if (app.Environment.IsDevelopment())
        {
            app.ApplyMigrations<TDbContext>();
        }

        UseDefaults(app, useNswag);

        return app;
    }
}
