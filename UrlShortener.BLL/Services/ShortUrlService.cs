using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.DTOs.ShortUrl.Request;
using UrlShortener.BLL.DTOs.ShortUrl.Response;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.InterfacesServices;
using UrlShortener.DAL.ApplicationDbContext;
using UrlShortener.DAL.InterfacesRepositories;
using UrlShortener.DAL.Models;
using UrlShortener.DAL.Repositories;

namespace UrlShortener.BLL.Services
{
    [RegisterClassAsTransient]
    public class ShortUrlService: IShortUrlService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGeneratorsShortCodeService _generatorsShortCodeService;

        public ShortUrlService(IUnitOfWork unitOfWork, IMapper mapper, IGeneratorsShortCodeService generatorsShortCodeService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _generatorsShortCodeService = generatorsShortCodeService;
        }

        public async Task<IEnumerable<GeneralShortURLDTO>> GetAllAsync()
        {
            var urls = await _unitOfWork.ShortUrlRepository.GetAsync();
            return _mapper.Map<IEnumerable<GeneralShortURLDTO>>(urls);
        }

        public async Task<ShortUrl> CreateAsync(AddShortUrlDTO shortUrlDTO)
        {
            if (string.IsNullOrWhiteSpace(shortUrlDTO.OriginalUrl))
                throw new ArgumentException("Original URL cannot be empty.", nameof(shortUrlDTO.OriginalUrl));

            var existing = await _unitOfWork.ShortUrlRepository
                .GetAsync(s => s.OriginalUrl == shortUrlDTO.OriginalUrl);

            if (existing.Any())
                throw new ItemExistException("This URL has already been shortened.");

            string shortCode;
            do
            {
                shortCode = _generatorsShortCodeService.GenerateShortCode();
                var existingCode = await _unitOfWork.ShortUrlRepository
                    .GetAsync(s => s.ShortCode == shortCode);
                if (!existingCode.Any())
                    break;
            } while (true);

            var shortUrl = new ShortUrl
            {
                OriginalUrl = shortUrlDTO.OriginalUrl,
                ShortCode = shortCode,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = shortUrlDTO.CreatedByUserId
            };

            await _unitOfWork.ShortUrlRepository.InsertAsync(shortUrl);
            await _unitOfWork.SaveAsync();

            return shortUrl;
        }
    }
}
