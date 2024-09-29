using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A5.Models
{
    public class ShowWithDetailViewModel : ShowBaseViewModel
    {
        [Display(Name = "Cast")]
        public IEnumerable<ActorBaseViewModel> Actors { get; set; }

        [Display(Name = "Episodes")]
        public IEnumerable<EpisodeBaseViewModel> Episodes { get; set; }
    }
}