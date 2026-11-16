using System;

namespace MusicDataModels
{
    public class Song
    {
        public int SongID { get; set; }
        public string SongName { get; set; }
        public int SongArtistID { get; set; }
        public int SongDecadeID { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
