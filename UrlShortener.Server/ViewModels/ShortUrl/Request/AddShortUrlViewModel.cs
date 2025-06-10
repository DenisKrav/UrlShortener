namespace UrlShortener.Server.ViewModels.ShortUrl.Request
{
    public class AddShortUrlViewModel
    {
        public string? OriginalUrl { get; set; }
        public long CreatedByUserId { get; set; }
    }
}
