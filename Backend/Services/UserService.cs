using System;
using System.Threading.Tasks;
using FunctionApp1.Context;
using FunctionApp1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FunctionApp1.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<object> GetAllUsers()
    {
        try
        {
            Console.WriteLine("┌────────────────────────────┐");
            Console.WriteLine("│ ██ UserService.GetAllUsers │");
            Console.WriteLine("└────────────────────────────┘");

            return await _context.Users.ToListAsync();
        }
        catch (Exception exc)
        {
            // if you have a logging mechanism, implement here
            throw;
        }
    }
}