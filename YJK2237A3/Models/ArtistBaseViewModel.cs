using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A3.Models
{
    public class ArtistBaseViewModel
    {
        [Key]
        [Display(Name = "MediaTypeId")]
        public int MediaTypeId { get; set; }

        [Display(Name = "Album Name")]
        public string Name { get; set; }
    }
}