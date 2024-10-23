using System;
using System.Collections.Generic;
using System.Linq;
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
        
        // See here for seeding: https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
 
        // ██████████████████████████
        // ██ Note pour moi-même:
        // ██ J'avais installé une Release Candidate (Version="9.0.0-rc.2.24474.1")
        // ██ 
        // ██ A cause d'un problème de compilation à l'exécution de la commande 'func host start', 
        // ██ obligé de revenir temporairement à la dernière version officelle.
        // ██ du coup, obligé de mettre le code ci-dessous en commentaire.
        // ██████████████████████████
        // optionsBuilder.UseSeeding((context, _) =>
        // {
        //     Console.WriteLine("┌─────────────────────────────────────────────────────┐");
        //     Console.WriteLine("│ ██ ApplicationDbContext.OnConfiguring -> UseSeeding │");
        //     Console.WriteLine("└─────────────────────────────────────────────────────┘");

        //     if (!context.Set<State>().Any())
        //     {
        //         var states = GetStates();
        //         context.Set<State>().AddRange(states);

        //         try
        //         {
        //             int numAffected = context.SaveChanges();
        //             Console.WriteLine($"██████ Saved {numAffected} states");
        //         }
        //         catch (Exception exp)
        //         {                
        //             Console.WriteLine($"██████ Error in {nameof(ApplicationDbContext)}: " + exp.Message);
        //             throw; 
        //         }
        //     }

        //     if (!context.Set<Customer>().Any())
        //     {
        //         var states = GetStates();
        //         var customers = GetCustomers(states);
        //         context.Set<Customer>().AddRange(customers);

        //         try
        //         {
        //             int numAffected = context.SaveChanges();
        //             Console.WriteLine($"██████ Saved {numAffected} customers");
        //         }
        //         catch (Exception exp)
        //         {
        //             Console.WriteLine($"██████ Error in {nameof(ApplicationDbContext)}: " + exp.Message);
        //             throw;
        //         }
        //     }
        // });
        
        // optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
        // {
        //     Console.WriteLine("┌──────────────────────────────────────────────────────────┐");
        //     Console.WriteLine("│ ██ ApplicationDbContext.OnConfiguring -> UseAsyncSeeding │");
        //     Console.WriteLine("└──────────────────────────────────────────────────────────┘");

        //     if (!await context.Set<State>().AnyAsync())
        //     {
        //         var states = GetStates();
        //         await context.Set<State>().AddRangeAsync(states);

        //         try
        //         {
        //             int numAffected = await context.SaveChangesAsync();
        //             Console.WriteLine(@"██████ Saved {numAffected} states");
        //         }
        //         catch (Exception exp)
        //         {                
        //             Console.WriteLine($"██████ Error in {nameof(ApplicationDbContext)}: " + exp.Message);
        //             throw; 
        //         }
        //     }

        //     if (!await context.Set<Customer>().AnyAsync())
        //     {
        //         var states = GetStates();
        //         var customers = GetCustomers(states);
        //         await context.Set<Customer>().AddRangeAsync(customers);

        //         try
        //         {
        //             int numAffected = await context.SaveChangesAsync();
        //             Console.WriteLine($"██████ Saved {numAffected} customers");
        //         }
        //         catch (Exception exp)
        //         {
        //             Console.WriteLine($"██████ Error in {nameof(ApplicationDbContext)}: " + exp.Message);
        //             throw;
        //         }
        //     }
        // });
    }

    private List<State> GetStates() {
        
            var states = new List<State> 
            {
                new State { Name = "Alabama", Abbreviation = "AL" },
                new State { Name = "Montana", Abbreviation = "MT" },
                new State { Name = "Alaska", Abbreviation = "AK" },
                new State { Name = "Nebraska", Abbreviation = "NE" },
                new State { Name = "Arizona", Abbreviation = "AZ" },
                new State { Name = "Nevada", Abbreviation = "NV" },
                new State { Name = "Arkansas", Abbreviation = "AR" },
                new State { Name = "New Hampshire", Abbreviation = "NH" },
                new State { Name = "California", Abbreviation = "CA" },
                new State { Name = "New Jersey", Abbreviation = "NJ" },
                new State { Name = "Colorado", Abbreviation = "CO" },
                new State { Name = "New Mexico", Abbreviation = "NM" },
                new State { Name = "Connecticut", Abbreviation = "CT" },
                new State { Name = "New York", Abbreviation = "NY" },
                new State { Name = "Delaware", Abbreviation = "DE" },
                new State { Name = "North Carolina", Abbreviation = "NC" },
                new State { Name = "Florida", Abbreviation = "FL" },
                new State { Name = "North Dakota", Abbreviation = "ND" },
                new State { Name = "Georgia", Abbreviation = "GA" },
                new State { Name = "Ohio", Abbreviation = "OH" },
                new State { Name = "Hawaii", Abbreviation = "HI" },
                new State { Name = "Oklahoma", Abbreviation = "OK" },
                new State { Name = "Idaho", Abbreviation = "ID" },
                new State { Name = "Oregon", Abbreviation = "OR" },
                new State { Name = "Illinois", Abbreviation = "IL" },
                new State { Name = "Pennsylvania", Abbreviation = "PA" },
                new State { Name = "Indiana", Abbreviation = "IN" },
                new State { Name = "Rhode Island", Abbreviation = "RI" },
                new State { Name = "Iowa", Abbreviation = "IA" },
                new State { Name = "South Carolina", Abbreviation = "SC" },
                new State { Name = "Kansas", Abbreviation = "KS" },
                new State { Name = "South Dakota", Abbreviation = "SD" },
                new State { Name = "Kentucky", Abbreviation = "KY" },
                new State { Name = "Tennessee", Abbreviation = "TN" },
                new State { Name = "Louisiana", Abbreviation = "LA" },
                new State { Name = "Texas", Abbreviation = "TX" },
                new State { Name = "Maine", Abbreviation = "ME" },
                new State { Name = "Utah", Abbreviation = "UT" },
                new State { Name = "Maryland", Abbreviation = "MD" },
                new State { Name = "Vermont", Abbreviation = "VT" },
                new State { Name = "Massachusetts", Abbreviation = "MA" },
                new State { Name = "Virginia", Abbreviation = "VA" },
                new State { Name = "Michigan", Abbreviation = "MI" },
                new State { Name = "Washington", Abbreviation = "WA" },
                new State { Name = "Minnesota", Abbreviation = "MN" },
                new State { Name = "West Virginia", Abbreviation = "WV" },
                new State { Name = "Mississippi", Abbreviation = "MS" },
                new State { Name = "Wisconsin", Abbreviation = "WI" },
                new State { Name = "Missouri", Abbreviation = "MO" },
                new State { Name = "Wyoming", Abbreviation = "WY" }
            };

            return states;
        }

    private List<Customer> GetCustomers(List<State> states) {
            //Customers
            var customerNames = new string[]
            {
                "Marcus,HighTower,Male,acmecorp.com",
                "Jesse,Smith,Female,gmail.com",
                "Albert,Einstein,Male,outlook.com",
                "Dan,Wahlin,Male,yahoo.com",
                "Ward,Bell,Male,gmail.com",
                "Brad,Green,Male,gmail.com",
                "Igor,Minar,Male,gmail.com",
                "Miško,Hevery,Male,gmail.com",
                "Michelle,Avery,Female,acmecorp.com",
                "Heedy,Wahlin,Female,hotmail.com",
                "Thomas,Martin,Male,outlook.com",
                "Jean,Martin,Female,outlook.com",
                "Robin,Cleark,Female,acmecorp.com",
                "Juan,Paulo,Male,yahoo.com",
                "Gene,Thomas,Male,gmail.com",
                "Pinal,Dave,Male,gmail.com",
                "Fred,Roberts,Male,outlook.com",
                "Tina,Roberts,Female,outlook.com",
                "Cindy,Jamison,Female,gmail.com",
                "Robyn,Flores,Female,yahoo.com",
                "Jeff,Wahlin,Male,gmail.com",
                "Danny,Wahlin,Male,gmail.com",
                "Elaine,Jones,Female,yahoo.com",
                "John,Papa,Male,gmail.com"
            };
            var addresses = new string[]
            {
                "1234 Anywhere St.",
                "435 Main St.",
                "1 Atomic St.",
                "85 Cedar Dr.",
                "12 Ocean View St.",
                "1600 Amphitheatre Parkway",
                "1604 Amphitheatre Parkway",
                "1607 Amphitheatre Parkway",
                "346 Cedar Ave.",
                "4576 Main St.",
                "964 Point St.",
                "98756 Center St.",
                "35632 Richmond Circle Apt B",
                "2352 Angular Way",
                "23566 Directive Pl.",
                "235235 Yaz Blvd.",
                "7656 Crescent St.",
                "76543 Moon Ave.",
                "84533 Hardrock St.",
                "5687534 Jefferson Way",
                "346346 Blue Pl.",
                "23423 Adams St.",
                "633 Main St.",
                "899 Mickey Way"
            };

            var citiesStates = new string[]
            {
                "Phoenix,AZ,Arizona",
                "Encinitas,CA,California",
                "Seattle,WA,Washington",
                "Chandler,AZ,Arizona",
                "Dallas,TX,Texas",
                "Orlando,FL,Florida",
                "Carey,NC,North Carolina",
                "Anaheim,CA,California",
                "Dallas,TX,Texas",
                "New York,NY,New York",
                "White Plains,NY,New York",
                "Las Vegas,NV,Nevada",
                "Los Angeles,CA,California",
                "Portland,OR,Oregon",
                "Seattle,WA,Washington",
                "Houston,TX,Texas",
                "Chicago,IL,Illinois",
                "Atlanta,GA,Georgia",
                "Chandler,AZ,Arizona",
                "Buffalo,NY,New York",
                "Albuquerque,AZ,Arizona",
                "Boise,ID,Idaho",
                "Salt Lake City,UT,Utah",
                "Orlando,FL,Florida"
            };

            var citiesIds = new int[] {5, 9, 44, 5, 36, 17, 16, 9, 36, 14, 14, 6, 9, 24, 44, 36, 25, 19, 5, 14, 5, 23, 38, 17};
            var zip = 85229;
            var customers = new List<Customer>();
            var random = new Random();

            for (var i = 0; i < customerNames.Length; i++) {
                var nameGenderHost = customerNames[i].Split(',');
                var cityState = citiesStates[i].Split(',');
                var state = states.Where(s => s.Abbreviation == cityState[1]).SingleOrDefault();

                var customer = new Customer {
                    FirstName = nameGenderHost[0],
                    LastName = nameGenderHost[1],
                    Email = nameGenderHost[0] + '.' + nameGenderHost[1] + '@' + nameGenderHost[3],
                    Address = addresses[i],
                    City = cityState[0],
                    State = state,
                    Zip = zip + i,
                    Gender = nameGenderHost[2],
                    OrderCount = 0
                };

                customers.Add(customer);
            }

            return customers;
        }


}