using System;
using System.Collections.Generic;

#nullable disable

namespace MusicDataAccess.Model
{
    public partial class MusicSongSuggestion
    {
        public int SuggestedSongId { get; set; }
        public string SuggestedSongArtist { get; set; }
        public string SuggestedSongTitle { get; set; }
        public int SuggestedSongDecade { get; set; }
    }
}
