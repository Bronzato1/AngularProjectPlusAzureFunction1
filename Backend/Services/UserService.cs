using System;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

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