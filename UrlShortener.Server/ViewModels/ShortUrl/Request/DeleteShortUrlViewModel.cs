namespace UrlShortener.Server.ViewModels.ShortUrl.Request
{
    public class DeleteShortUrlViewModel
    {
        public int LinkId { get; set; }
        public long UserId { get; set; }
    }
}
