using blazormovie.Shared.DTOs;
using System.Threading.Tasks;

namespace blazormovie.Client.Auth
{
    public interface ILoginService
    {
        Task Login(UserToken userToken);
        Task Logout();
        Task ManejarRenovacionToken();
    }
}
