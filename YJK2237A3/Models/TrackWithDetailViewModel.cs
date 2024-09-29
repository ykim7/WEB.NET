using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A3.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }

        [Display(Name ="Media type")]
        public MediaTypeBaseViewModel MediaType { get; set; }
    }
}