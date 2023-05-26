using System.Reflection;
using System.Runtime.InteropServices;
using Api;
using Application;
using Application.HttpClients;
using Application.Options;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventLog;
using TourmalineCore.AspNetCore.JwtAuthentication.Core;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Options;

const string debugEnvironmentName = "Debug";
const string loggingSectionKey = "Logging";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
            config.AddCommandLine(args);
        }
    );

var configuration = builder.Configuration;

var authenticationOptions = configuration.GetSection(nameof(AuthenticationOptions)).Get<AuthenticationOptions>();
builder.Services.AddJwtAuthentication(authenticationOptions).WithUserClaimsProvider<UserClaimsProvider>(UserClaimsProvider.PermissionClaimType);

builder.Services.AddPersistence(configuration);
builder.Services.AddApplication();

builder.Host.ConfigureLogging((hostingContext, logging) =>
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                logging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Warning);
            }

            logging.AddConfiguration(hostingContext.Configuration.GetSection(loggingSectionKey));
            logging.AddConsole();
            logging.AddDebug();
            logging.AddEventSourceLogger();

            if (isWindows)
            {
                logging.AddEventLog();
            }

            logging.Configure(options =>
                    {
                        options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
                                                          | ActivityTrackingOptions.TraceId
                                                          | ActivityTrackingOptions.ParentId;
                    }
                );
        }
    );

builder.Services.Configure<HttpUrls>(configuration.GetSection(nameof(HttpUrls)));
builder.Services.Configure<AccountValidationOptions>(configuration.GetSection(nameof(AccountValidationOptions)));

var app = builder.Build();

app.UseCors(
        corsPolicyBuilder => corsPolicyBuilder
            .AllowAnyHeader()
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyOrigin()
    );

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (app.Environment.IsEnvironment(debugEnvironmentName))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AccountsDbContext>();
    await context.Database.MigrateAsync();
}

app.UseRouting();
app.UseJwtAuthentication();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();