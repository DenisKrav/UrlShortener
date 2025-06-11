namespace UrlShortener.BLL.DTOs.ShortUrl.Request
{
    public class DeleteShortUrlDTO
    {
        public int LinkId { get; set; }
        public long UserId { get; set; }
    }
}
