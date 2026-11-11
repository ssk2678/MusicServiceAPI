using System;
using System.Collections.Generic;

namespace MusicDataAccess.Model;

public partial class MusicArtist
{
    public int ArtistId { get; set; }

    public string ArtistName { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<MusicSong> MusicSongs { get; set; } = new List<MusicSong>();

    public virtual ICollection<MusicStatistic> MusicStatistics { get; set; } = new List<MusicStatistic>();
}
