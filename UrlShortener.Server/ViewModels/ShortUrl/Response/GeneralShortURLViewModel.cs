using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Server.ViewModels.ShortUrl.Response
{
    public class GeneralShortURLViewModel
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedByUserId { get; set; }

        public string CreatedByUserFirstName { get; set; }
        public string CreatedByUserLastName { get; set; }
    }
}
