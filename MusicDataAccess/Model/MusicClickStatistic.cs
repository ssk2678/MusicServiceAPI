using System;
using System.Collections.Generic;

#nullable disable

namespace MusicDataAccess.Model
{
    public partial class MusicClickStatistic
    {
        public int MusicClickStatId { get; set; }
        public int MusicClickStatDecadeId { get; set; }
        public int MusicClickStatArtistId { get; set; }
        public int MusicClickStatSongId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual MusicArtist MusicClickStatArtist { get; set; }
        public virtual MusicDecade MusicClickStatDecade { get; set; }
        public virtual MusicSong MusicClickStatSong { get; set; }
    }
}
