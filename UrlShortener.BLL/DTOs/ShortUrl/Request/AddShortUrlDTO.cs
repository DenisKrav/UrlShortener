using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.BLL.DTOs.ShortUrl.Request
{
    public class AddShortUrlDTO
    {
        public string? OriginalUrl { get; set; }
        public long CreatedByUserId { get; set; }
    }
}
