using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.DAL.InterfacesRepositories
{
    public interface IUnitOfWork
    {
        IAboutInfoRepository AboutInfoRepository { get; }
        IShortUrlRepository ShortUrlRepository { get; }

        Task SaveAsync();
    }
}
