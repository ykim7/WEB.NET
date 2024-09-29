using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YJK2237A2.Models
{
    public class InvoiceLineWithDetailViewModel : InvoiceLineBaseViewModel
    {
        public string TrackName { get; set; }
        public string Composer { get; set; }
        public string AlbumTitle { get; set; }
        public string ArtistName { get; set; }
        public string GenreName { get; set; }
        public string MediaTypeName { get; set; }
    }
}