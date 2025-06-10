using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.DTOs.ShortUrl.Response;
using UrlShortener.DAL.Models;

namespace UrlShortener.BLL.Mappers
{
    public class ShortURLDTOProfile: Profile
    {
        public ShortURLDTOProfile()
        {
            CreateMap<ShortUrl, GeneralShortURLDTO>();
        }
    }
}
