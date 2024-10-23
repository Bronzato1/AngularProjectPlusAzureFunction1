using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repository
{
    public interface IStateRepository
    {
        Task<List<State>> GetStatesAsync();
    }
}