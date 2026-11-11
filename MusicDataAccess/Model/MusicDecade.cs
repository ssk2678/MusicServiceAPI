using System;
using System.Collections.Generic;

namespace MusicDataAccess.Model;

public partial class MusicDecade
{
    public int DecadeId { get; set; }

    public string DecadeName { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<MusicSong> MusicSongs { get; set; } = new List<MusicSong>();

    public virtual ICollection<MusicStatistic> MusicStatistics { get; set; } = new List<MusicStatistic>();
}
