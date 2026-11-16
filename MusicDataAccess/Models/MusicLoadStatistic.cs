using System;

namespace MusicDataAccess.Models;

public partial class MusicLoadStatistic
{
    public int MusicLoadStatId { get; set; }

    public int MusicLoadMusicCountId { get; set; }

    public int MusicLoadStatCountSeventies { get; set; }

    public int MusicLoadStatCountEighties { get; set; }

    public int MusicLoadStatCountNineties { get; set; }

    public int MusicLoadStatCountTwoThousands { get; set; }

    public int MusicLoadStatCountTwoThousandTens { get; set; }

    public int MusicLoadStatCountTwoThousandTwenties { get; set; }

    public int MusicLoadStatCountSongs { get; set; }

    public int MusicLoadStatCountArtists { get; set; }

    public int MusicLoadStatCountSeventiesAdded { get; set; }

    public int MusicLoadStatCountEightiesAdded { get; set; }

    public int MusicLoadStatCountNinetiesAdded { get; set; }

    public int MusicLoadStatCountTwoThousandsAdded { get; set; }

    public int MusicLoadStatCountTwoThousandTensAdded { get; set; }

    public int MusicLoadStatCountTwoThousandTwentiesAdded { get; set; }

    public int MusicLoadStatCountSongsAdded { get; set; }

    public int MusicLoadStatCountArtistsAdded { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual MusicCount MusicLoadMusicCount { get; set; }
}
