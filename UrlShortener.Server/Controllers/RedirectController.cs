using Microsoft.AspNetCore.Mvc;
using UrlShortener.BLL.InterfacesServices;
using UrlShortener.DAL.InterfacesRepositories;

namespace UrlShortener.Server.Controllers
{
    [ApiController]
    [Route("{shortCode}")]
    public class RedirectController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;

        public RedirectController(IShortUrlService shortUrlRepository)
        {
            _shortUrlService = shortUrlRepository;
        }

        [HttpGet]
        public async Task<IActionResult> RedirectToOriginal(string shortCode)
        {
            var shortUrl = await _shortUrlService.GetOriginUrlByShortUrl(shortCode);

            if (shortUrl == null)
            {
                return NotFound("Short URL not found.");
            }

            return Redirect(shortUrl);
        }
    }

}
