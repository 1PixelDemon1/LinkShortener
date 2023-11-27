using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortWeb.Model.Models
{
    public class ShortLink
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Shortened Link")]
        public string ShortenedLink { get; set; }
        [Required]
        public string Link { get; set; }
    }
}
