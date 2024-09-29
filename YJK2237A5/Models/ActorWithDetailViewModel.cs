using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A5.Models
{
    public class ActorWithDetailViewModel: ActorBaseViewModel
    {
        [Display(Name = "Appeared In")]
        public IEnumerable<ShowBaseViewModel> Shows { get; set; }

    }
}