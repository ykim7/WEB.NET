using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YJK2237A2.Models
{
    public class InvoiceWithDetailViewModel : InvoiceBaseViewModel
    {
        // Composed properties for Customer

        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerState { get; set; }
        public string CustomerCountry { get; set; }

        // Composed properties for Employee (Customer's sales rep)

        public string CustomerEmployeeLastName { get; set; }
        public string CustomerEmployeeFirstName { get; set; }

        public IEnumerable<InvoiceLineWithDetailViewModel> InvoiceLines { get; set; }
    }
}