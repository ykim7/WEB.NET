using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A3.Models
{
    public class TrackBaseViewModel
    {
        [Key]
        public int TrackId { get; set; }

        [Required]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Composer")]
        public string Composer { get; set; }

        [Required]
        [Display(Name = "Length(ms)")]
        public int Milliseconds { get; set; }

        [Required]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        // Composed read-only property to display full name.
        public string NameFull
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);
                var composer = string.IsNullOrEmpty(Composer) ? "" : ", composer " + Composer; var trackLength = (ms > 0) ? ", " + ms.ToString() + " minutes" : "";
                var unitPrice = (UnitPrice > 0) ? ", $ " + UnitPrice.ToString() : "";
                return string.Format("{0}{1}{2}{3}", Name, composer, trackLength, unitPrice);
            }
        }
        // Composed read-only property to display short name.
        public string NameShort
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);
                var trackLength = (ms > 0) ? ms.ToString() + " minutes" : "";
                var unitPrice = (UnitPrice > 0) ? " $ " + UnitPrice.ToString() : "";
                return string.Format("{0} - {1} - {2}", Name, trackLength, unitPrice);
            }
        }
    }
}