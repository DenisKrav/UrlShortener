using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DAL.ApplicationDbContext;
using UrlShortener.DAL.InterfacesRepositories;
using UrlShortener.DAL.Models;

namespace UrlShortener.DAL.Repositories
{
    public class ShortUrlRepository: GenericRepository<ShortUrl>, IShortUrlRepository
    {
        public ShortUrlRepository(AppDbContext context) : base(context)
        {
        }
    }
}
