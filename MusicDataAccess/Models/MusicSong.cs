using System;
using System.Collections.Generic;

namespace MusicDataAccess.Models;

public partial class MusicSong
{
    public int SongId { get; set; }

    public string SongName { get; set; }

    public int? SongArtistId { get; set; }

    public int? SongDecadeId { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<MusicClickStatistic> MusicClickStatistics { get; set; } = new List<MusicClickStatistic>();

    public virtual MusicArtist SongArtist { get; set; }

    public virtual MusicDecade SongDecade { get; set; }
}
