using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A5.Models
{
    public class GenreBaseViewModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string name { get; set; }

    }
}