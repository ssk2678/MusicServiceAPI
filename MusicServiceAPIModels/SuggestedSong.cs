using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDataModels
{
    public class SongSuggestion
    {
        public int SuggestedSongId { get; set; }
        public string SuggestedSongArtist { get; set; }
        public string SuggestedSongTitle { get; set; }
        public int SuggestedSongDecade { get; set; }

    }
}
