using Backend.Context;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly ILogger _Logger;

        public StateRepository(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger("StateRepository");
        }

        public async Task<List<State>> GetStatesAsync()
        {
            return await _Context.States.OrderBy(s => s.Abbreviation).ToListAsync();
        }
    }
}
