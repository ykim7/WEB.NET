using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A3.Models
{
    public class TrackAddViewModel : TrackBaseViewModel
    {
        [Required]
        public int SelectedAlbumId { get; set; }

        [Required]
        public int SelectedMediaTypeId { get; set; }
    }
}