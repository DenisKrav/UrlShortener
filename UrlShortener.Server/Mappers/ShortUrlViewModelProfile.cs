using AutoMapper;
using UrlShortener.BLL.DTOs.Auth.Request;
using UrlShortener.BLL.DTOs.ShortUrl.Request;
using UrlShortener.BLL.DTOs.ShortUrl.Response;
using UrlShortener.BLL.Mappers;
using UrlShortener.DAL.Models;
using UrlShortener.Server.ViewModels.Auth.Request;
using UrlShortener.Server.ViewModels.ShortUrl.Request;
using UrlShortener.Server.ViewModels.ShortUrl.Response;

namespace UrlShortener.Server.Mappers
{
    public class ShortUrlViewModelProfile : Profile
    {
        public ShortUrlViewModelProfile()
        {
            CreateMap<AddShortUrlViewModel, AddShortUrlDTO>();
            CreateMap<ShortUrl, GeneralShortURLViewModel>();
            CreateMap<GeneralShortURLDTO, GeneralShortURLViewModel>();
        }
    }
}
