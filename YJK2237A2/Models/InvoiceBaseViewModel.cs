using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A2.Models
{
    public class InvoiceBaseViewModel
    {
        [Key]
        public int InvoiceId { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [Display(Name = "City")]
        public string BillingCity { get; set; }

        [Display(Name = "State")]
        public string BillingState { get; set; }

        [Display(Name = "Country")]
        public string BillingCountry { get; set; }

        [Display(Name = "Postal/Zip")]
        public string BillingPostalCode { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Total { get; set; }

    }

}