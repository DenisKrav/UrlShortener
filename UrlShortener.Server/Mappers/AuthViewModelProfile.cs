using AutoMapper;
using UrlShortener.BLL.DTOs.Auth.Request;
using UrlShortener.Server.ViewModels.Auth.Request;

namespace UrlShortener.Server.Mappers
{
    public class AuthViewModelProfile: Profile
    {
        public AuthViewModelProfile()
        {
            CreateMap<LoginRequestViewModel, LoginRequestDto>();
        }
    }
}
