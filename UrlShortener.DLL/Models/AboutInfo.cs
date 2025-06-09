using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DAL.Models.Identities;

namespace UrlShortener.DAL.Models
{
    public class AboutInfo
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        public DateTime LastEdited { get; set; } = DateTime.UtcNow;

        public long? EditedByUserId { get; set; }

        [ForeignKey(nameof(EditedByUserId))]
        public ApplicationUser? EditedByUser { get; set; }
    }
}
