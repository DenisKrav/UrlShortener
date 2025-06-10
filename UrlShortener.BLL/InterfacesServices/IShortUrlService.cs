using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.DTOs.ShortUrl.Request;
using UrlShortener.BLL.DTOs.ShortUrl.Response;
using UrlShortener.DAL.Models;

namespace UrlShortener.BLL.InterfacesServices
{
    public interface IShortUrlService
    {
        Task<IEnumerable<GeneralShortURLDTO>> GetAllAsync();

        Task<ShortUrl> CreateAsync(AddShortUrlDTO shortUrlDTO);
    }
}
