using AutoDependencyRegistration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DAL.ApplicationDbContext;
using UrlShortener.DAL.InterfacesRepositories;

namespace UrlShortener.DAL.Repositories
{
    [RegisterClassAsScoped]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IAboutInfoRepository _aboutInfoRepository;
        private IShortUrlRepository _shortUrlRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context; 
        }

        public IAboutInfoRepository AboutInfoRepository => _aboutInfoRepository ??= new AboutInfoRepository(_context);
        public IShortUrlRepository ShortUrlRepository => _shortUrlRepository ??= new ShortUrlRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
