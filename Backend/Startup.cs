using System;
using FunctionApp1.Context;
using FunctionApp1.Interfaces;
using FunctionApp1.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FunctionApp1.Startup))]

namespace FunctionApp1;

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

		var SQLConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString");
		
		builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(SQLConnectionString));

		builder.Services.AddScoped<IUserService, UserService>();
    }
}
