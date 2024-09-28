using System.Threading.Tasks;

namespace FunctionApp1.Interfaces;

public interface IUserService
{
    Task<object> GetAllUsers();
}