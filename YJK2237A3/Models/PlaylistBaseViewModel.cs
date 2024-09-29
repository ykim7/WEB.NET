using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A3.Models
{
    public class PlaylistBaseViewModel
    {
        public int PlaylistId { get; set; }

        [Required]
        [Display(Name = "Playlist Name")]
        public string Name { get; set; }

        public List<TrackBaseViewModel> Tracks { get; set; }
    }
}