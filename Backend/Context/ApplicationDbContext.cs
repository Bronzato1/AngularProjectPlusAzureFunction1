using System;
using FunctionApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace FunctionApp1.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {

    }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

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

        var SQLConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString");

        optionsBuilder.UseSqlServer(SQLConnectionString);
    }
}