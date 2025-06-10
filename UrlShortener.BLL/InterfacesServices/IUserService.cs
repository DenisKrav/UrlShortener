using UrlShortener.BLL.DTOs.User.Response;

namespace UrlShortener.BLL.InterfacesServices
{
    public interface IUserService
    {
        Task<UserDTO?> GetUserByIdAsync(long userId);

        Task<UserDTO?> GetUserByEmailAsync(string email);

        Task<UserDTO?> GetUserByPhoneAsync(string phone);

        Task<string> GetNameRoleByUserIdAsync(long userId);
    }
}
