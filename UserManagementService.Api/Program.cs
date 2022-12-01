using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventLog;
using UserManagementService.Application;
using UserManagementService.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddControllers();

builder.Services.AddApplication();

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;

    var reloadOnChange = hostingContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange", true);

    config.AddJsonFile("appsettings.json", true, reloadOnChange)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, reloadOnChange)
        .AddJsonFile("appsettings.Active.json", true, reloadOnChange);

    if (env.IsDevelopment() && !string.IsNullOrEmpty(env.ApplicationName))
    {
        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));

        config.AddUserSecrets(appAssembly, true);
    }

    config.AddEnvironmentVariables();

    if (args != null)
    {
        config.AddCommandLine(args);
    }
});

builder.Services.AddPersistence(configuration);

builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    // IMPORTANT: This needs to be added *before* configuration is loaded, this lets
    // the defaults be overridden by the configuration.
    if (isWindows)
    {
        // Default the EventLogLoggerProvider to warning or above
        logging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Warning);
    }

    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventSourceLogger();

    if (isWindows)
    {
        // Add the EventLogLoggerProvider on windows machines
        logging.AddEventLog();
    }

    logging.Configure(options =>
    {
        options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
                                          | ActivityTrackingOptions.TraceId
                                          | ActivityTrackingOptions.ParentId;
    }
        );
});

var app = builder.Build();

if (environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<UsersDbContext>();
    await context.Database.MigrateAsync();
}

app.UseRouting();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
