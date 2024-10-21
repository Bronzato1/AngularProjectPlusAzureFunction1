using System;
using Backend.Context;
using Backend.Interfaces;
using Backend.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Backend.Startup))]

namespace Backend;

// https://learn.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
		Console.WriteLine("┌──────────────────────┐");
		Console.WriteLine("│ ██ Startup.Configure │");
		Console.WriteLine("└──────────────────────┘");

		// Code in this function used to configure the db context + dependency injection.
		// This startup file is executed when starting the application normally.
		// This startup file is not executed with instructions from the terminal.
        // local.settings.json file is accessible and queryable.

		var isLocal = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development"; // by default set to "Development" locally. Note that when deployed/published on Azure, you'd need to set the environment variable yourself
		var variable = isLocal ? "ConnectionStrings:ONE_PLUS_ONE" : "SQLAZURECONNSTR_ONE_PLUS_ONE"; //  Why prefixed with SQLAZURECONNSTR_... ? See here: https://learn.microsoft.com/en-us/azure/app-service/configure-common?tabs=portal#configure-connection-strings
		var SQLConnectionString = Environment.GetEnvironmentVariable(variable);

		builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(SQLConnectionString));

		builder.Services.AddScoped<IUserService, UserService>();
    }
}
