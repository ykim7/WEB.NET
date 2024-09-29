using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YJK2237A5.Models
{
    public class EpisodeVideoViewModel
    {

        public int Id { get; set; }
        public byte[] VideoByte { get; set; }
        public string VideoContentType { get; set; }

    }
}