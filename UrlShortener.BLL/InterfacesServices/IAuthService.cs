using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.DTOs.Auth.Request;

namespace UrlShortener.BLL.InterfacesServices
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequestDto loginRequest);

        Task<string> GenerateToken(string userId, string userEmail);
    }
}
