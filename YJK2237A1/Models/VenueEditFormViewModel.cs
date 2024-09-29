using System;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A1.Models
{
    public class VenueEditFormViewModel
    {
        [Key]
        public int VenueId { get; set; }

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
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(60)]
        [Display(Name = "Website")]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Open Date")]
        public DateTime? OpenDate { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Advance Ticket Sale Password")]
        public string TicketSalePassword { get; set; }

        [RegularExpression(@"^[A-Z]{2}\d{3}$", ErrorMessage = "Invalid Promo Code format. It must be in the format 'LLNNN'.")]
        [Display(Name = "Promo Code")]
        public string PromoCode { get; set; }

        [Range(1, 100000, ErrorMessage = "Capacity must be between 1 and 100,000.")]
        [Display(Name = "Capacity")]
        public int? Capacity { get; set; }
    }
}
