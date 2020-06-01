using Shopping.Shared.Model.Account;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel model);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel model);
    }
}