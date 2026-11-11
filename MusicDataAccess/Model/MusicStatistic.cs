using System;
using System.Collections.Generic;

namespace MusicDataAccess.Model;

public partial class MusicStatistic
{
    public int MusicStatId { get; set; }

    public int MusicStatDecadeId { get; set; }

    public int MusicStatArtistId { get; set; }

    public int MusicStatSongId { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual MusicArtist MusicStatArtist { get; set; }

    public virtual MusicDecade MusicStatDecade { get; set; }

    public virtual MusicSong MusicStatSong { get; set; }
}
