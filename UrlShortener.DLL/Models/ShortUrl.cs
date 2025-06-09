using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UrlShortener.DAL.Models.Identities;

namespace UrlShortener.DAL.Models
{
    public class ShortUrl
    {
        public int Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; } = null!;

        [Required]
        public string ShortCode { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public long CreatedByUserId { get; set; }

        [ForeignKey(nameof(CreatedByUserId))]
        public ApplicationUser CreatedByUser { get; set; } = null!;
    }
}
