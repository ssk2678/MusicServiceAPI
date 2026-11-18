using System;
using System.Collections.Generic;

namespace MusicDataAccess.Models;

public partial class SongSuggestion
{
    public int SuggestedSongId { get; set; }

    public string SuggestedSongArtist { get; set; }

    public string SuggestedSongTitle { get; set; }

    public int SuggestedSongDecade { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateModified { get; set; }

    public bool IsAdded { get; set; }
}
