using System;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A1.Models
{
    public class VenueAddViewModel
    {
        [Required]
        [StringLength(80)]
        [Display(Name = "Company Name")]
        public string Company { get; set; }

        [StringLength(70)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(40)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(40)]
        [Display(Name = "State")]
        public string State { get; set; }

        [StringLength(40)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [StringLength(10)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [StringLength(24)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [StringLength(24)]
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [StringLength(60)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(60)]
        [Display(Name = "Website")]
        public string Website { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Open Date")]
        public DateTime? OpenDate { get; set; } = DateTime.Now.AddYears(-23);
    }
}
