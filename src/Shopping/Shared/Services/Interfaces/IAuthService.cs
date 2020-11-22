using Shopping.Shared.Model.Account;
using System.Net.Http;
using System.Threading.Tasks;
using Shopping.Shared.Results;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel model);
        Task<RegisterResult> Register(RegisterModel model);
        Task<ChangePasswordResult> ChangePassword(ChangePasswordModel model);
        Task Logout();
        Task<DeleteUserResult> DeleteUser(DeleteUserModel model);
        Task<HttpClient> GetHttpClientAsync();
    }
}