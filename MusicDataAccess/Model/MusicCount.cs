using System;
using System.Collections.Generic;

namespace MusicDataAccess.Model;

public partial class MusicCount
{
    public int MusicCountId { get; set; }

    public int TotalSeventiesSongs { get; set; }

    public int TotalEightiesSongs { get; set; }

    public int TotalNinetiesSongs { get; set; }

    public int TotalTwoThousandsSongs { get; set; }

    public int TotalTwoThousandTensSongs { get; set; }

    public int TotalTwoThousandTwentiesSongs { get; set; }

    public int TotalSongs { get; set; }

    public int TotalArtists { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateModified { get; set; }
}
