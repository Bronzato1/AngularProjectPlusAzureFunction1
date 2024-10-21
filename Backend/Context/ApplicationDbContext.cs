using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {

    }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<State> States { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine("┌───────────────────────────────────────┐");
        Console.WriteLine("│ ██ ApplicationDbContext.OnConfiguring │");
        Console.WriteLine("└───────────────────────────────────────┘");

        // Function called to configure the db context.
        // When?
        // Whenever you execute code like below:
        // - dotnet ef migrations add InitialCreate
        // - dotnet ef database update
        // Warning! 
        // Don't forget to set environment variable first like below:
        // $env:ConnectionStrings:SQLConnectionString="Server=tcp:one-plus-one.database.windows.net,1433;Initial Catalog=one-plus-one_db;Persist Security Info=False;User ID=admin-test;Password=C0mplexPwd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
        // Why? 
        // Because local.settings.json is not accessible when executing from the terminal.

        var isLocal = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development"; // by default set to "Development" locally. Note that when deployed/published on Azure, you'd need to set the environment variable yourself
		var variable = isLocal ? "ConnectionStrings:ONE_PLUS_ONE" : "SQLAZURECONNSTR_ONE_PLUS_ONE"; //  Why prefixed with SQLAZURECONNSTR_... ? See here: https://learn.microsoft.com/en-us/azure/app-service/configure-common?tabs=portal#configure-connection-strings
		var SQLConnectionString = Environment.GetEnvironmentVariable(variable);

        optionsBuilder.UseSqlServer(SQLConnectionString);
    }
}