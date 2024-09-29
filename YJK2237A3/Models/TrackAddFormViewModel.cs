using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A3.Models
{
    public class TrackAddFormViewModel
    {
        [Required, StringLength(200)]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [Required]
        public int Milliseconds { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public SelectList AlbumSelectList { get; set; }
        public SelectList MediaTypeSelectList { get; set; }

        [Required]
        public int SelectedAlbumId { get; set; }

        [Required]
        public int SelectedMediaTypeId { get; set; }

    }


}