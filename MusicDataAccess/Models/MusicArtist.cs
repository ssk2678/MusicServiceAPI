using System;
using System.Collections.Generic;

namespace MusicDataAccess.Models;

public partial class MusicArtist
{
    public int ArtistId { get; set; }

    public string ArtistName { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<MusicClickStatistic> MusicClickStatistics { get; set; } = new List<MusicClickStatistic>();

    public virtual ICollection<MusicSong> MusicSongs { get; set; } = new List<MusicSong>();
}
