using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace YJK2237A5.Models
{
    public class EpisodeAddFormViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Season")]
        public int SeasonNumber { get; set; }

        [Required]
        [Display(Name = "Episode")]
        public int EpisodeNumber { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Data Aired")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AirDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Clerk")]
        public string Clerk { get; set; }

        [Required]
        public ShowBaseViewModel Show { get; set; }

        public SelectList GenreList { get; set; }

    }
}