using Microsoft.AspNetCore.Identity;
using UrlShortener.BLL.DTOs.User.Request;
using UrlShortener.BLL.DTOs.User.Response;

namespace UrlShortener.BLL.InterfacesServices
{
    public interface IUserService
    {
        Task<(IdentityResult, long)> CreateUserAsync(NewUserDTO newUserDTO);

        Task<IdentityResult> DeleteUserAsync(long userId);

        Task<IdentityResult> UpdateUserAsync(UpdateUserDTO updateUserDTO);

        Task<UserDTO?> GetUserByIdAsync(long userId);

        Task<UserDTO?> GetUserByEmailAsync(string email);

        Task<UserDTO?> GetUserByPhoneAsync(string phone);

        Task<string> GetNameRoleByUserIdAsync(long userId);
    }
}
