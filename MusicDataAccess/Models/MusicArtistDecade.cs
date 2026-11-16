using System;

namespace MusicDataAccess.Models;

public partial class MusicArtistDecade
{
    public int ArtistDecadeId { get; set; }

    public int? ArtistId { get; set; }

    public int? DecadeId { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateModified { get; set; }
}
