using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.BLL.DTOs.ShortUrl.Request;
using UrlShortener.BLL.Exceptions;
using UrlShortener.BLL.InterfacesServices;
using UrlShortener.DAL.InterfacesRepositories;
using UrlShortener.DAL.Models;
using UrlShortener.Server.Mappers;
using UrlShortener.Server.Utilities;
using UrlShortener.Server.ViewModels.ShortUrl.Request;
using UrlShortener.Server.ViewModels.ShortUrl.Response;

namespace UrlShortener.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlsController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;
        private readonly IMapper _mapper;

        public ShortUrlsController(IShortUrlService shortUrlService, IMapper mapper)
        {
            _shortUrlService = shortUrlService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<GeneralResultModel> GetAll()
        {
            var result = new GeneralResultModel();
            var shortUrls = await _shortUrlService.GetAllAsync();
            result.Result = _mapper.Map<IEnumerable<GeneralShortURLViewModel>>(shortUrls);

            return result;
        }

        [Authorize]
        [HttpPost("AddUrl")]
        public async Task<GeneralResultModel> AddUrl([FromBody] AddShortUrlViewModel shortUrlViewModel)
        {
            var result = new GeneralResultModel();

            if (string.IsNullOrWhiteSpace(shortUrlViewModel.OriginalUrl))
            {
                result.Errors.Add("Original URL is required.");
                return result;
            }

            if (shortUrlViewModel.CreatedByUserId <= 0)
            {
                result.Errors.Add("Author ID is required.");
                return result;
            }

            try
            {
                var createdShortUrl = await _shortUrlService.CreateAsync(_mapper.Map<AddShortUrlDTO>(shortUrlViewModel));
                result.Result = _mapper.Map<GeneralShortURLViewModel>(createdShortUrl);

                return result;
            }
            catch (ItemExistException ex)
            {
                result.Errors.Add(ex.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public async Task<GeneralResultModel> Delete([FromBody] DeleteShortUrlViewModel deleteShortUrlViewModel)
        {
            var result = new GeneralResultModel();

            if (deleteShortUrlViewModel.LinkId <= 0)
            {
                result.Errors.Add("Invalid link ID.");
                return result;
            }

            if (deleteShortUrlViewModel.UserId <= 0)
            {
                result.Errors.Add("Invalid user ID.");
                return result;
            }

            try
            {
                var resOfDel = await _shortUrlService.DeleteAsync(_mapper.Map<DeleteShortUrlDTO>(deleteShortUrlViewModel));
                result.Result = resOfDel; 
                return result;
            }
            catch (ItemNotFoundException ex)
            {
                result.Errors.Add(ex.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add("Unexpected error occurred.");
                return result;
            }
        }
    }
}
