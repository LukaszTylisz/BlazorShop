using BlazorShop.Application;
using BlazorShop.Infrastructure;
using BlazorShop.Infrastructure.Settings;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Logging
        .ClearProviders()
        .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

    builder.Host.UseNLog();

    builder.Services
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
        .Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseExceptionHandler("/Error");
    }

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");

    app.Run();
}

catch (Exception exception)
{
    logger.Error(exception, "Stopped program of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}