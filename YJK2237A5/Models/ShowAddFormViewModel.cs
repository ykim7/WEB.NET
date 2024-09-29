using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace YJK2237A5.Models
{
    public class ShowAddFormViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Required]
        public ActorWithDetailViewModel PreselectedActor { get; set; }

        public SelectList GenreList { get; set; }

        public MultiSelectList ActorList { get; set; }

        public IEnumerable<int> SelectedActorIds { get; set; }

        public IEnumerable<ActorBaseViewModel> Actors { get; set; }

        public IEnumerable<EpisodeBaseViewModel> Episodes { get; set; }


        [HiddenInput(DisplayValue = false)]
        public string Coordinator { get; set; }

        public string Premise { get; set; }

        public bool IsActorSelected(int actorId)
        {
            return SelectedActorIds != null && SelectedActorIds.Contains(actorId);
        }
    }
}
