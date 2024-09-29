using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A2.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        [Display(Name = "Album")]
        public string AlbumTitle { get; set; }

        [Display(Name = "Genre")]
        public string GenreName { get; set; }
    }


}