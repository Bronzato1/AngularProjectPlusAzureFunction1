using System.Threading.Tasks;

namespace Backend.Interfaces;

public interface IUserService
{
    Task<object> GetAllUsers();
}