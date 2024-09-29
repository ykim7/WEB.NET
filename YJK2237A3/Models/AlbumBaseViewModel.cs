using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A3.Models
{
    public class AlbumBaseViewModel
    {
        [Key]
        public int albumId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }
    }
}