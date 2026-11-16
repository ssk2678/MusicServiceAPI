using System;

namespace MusicDataModels
{
    public class Decade
    {
        public int DecadeID { get; set; }
        public string DecadeName { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
    }

    public enum Decades
    {
        Seventies = 1,
        Eighties = 2,
        Nineties = 3,
        TwoThousands = 4,
        TwoThousandTeens = 5
    }
}
