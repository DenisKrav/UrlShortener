using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DAL.Models;

namespace UrlShortener.DAL.InterfacesRepositories
{
    public interface IShortUrlRepository: IGenericRepository<ShortUrl>
    {
    }
}
