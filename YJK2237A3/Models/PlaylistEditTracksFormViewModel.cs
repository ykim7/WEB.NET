using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YJK2237A3.Models
{
    public class PlaylistEditTracksFormViewModel: PlaylistBaseViewModel
    {

        public List<TrackBaseViewModel> AllTracks { get; set; }

        public int[] SelectedTrackIds { get; set; }

    }
}