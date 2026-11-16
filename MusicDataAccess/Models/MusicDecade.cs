using System;
using System.Collections.Generic;

namespace MusicDataAccess.Models;

public partial class MusicDecade
{
    public int DecadeId { get; set; }

    public string DecadeName { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual ICollection<MusicClickStatistic> MusicClickStatistics { get; set; } = new List<MusicClickStatistic>();

    public virtual ICollection<MusicSong> MusicSongs { get; set; } = new List<MusicSong>();
}
