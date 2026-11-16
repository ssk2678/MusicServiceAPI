using System.Collections.Generic;
using System.Threading.Tasks;
using MusicDataAccess.Models;
using MusicDataModels;

namespace MusicDataAccess
{
    public interface IMusicDataRepository
    {
        Task<IEnumerable<Decade>> GetDecades();
        Task<Artist> GetArtistById(int artistID);
        Task<IEnumerable<Artist>> GetArtistsByDecade(int decadeID);
        Task<Song> GetSongById(int sontID);
        Task<IEnumerable<Song>> GetSongsByArtist(int artistID);
        Task<IEnumerable<Song>> GetSongsByArtistAndDecade(int artistID, int decadeID);
        Task<Artist> AddArtist(Artist a);
        Task<Song> AddSong(Song s);
        //Task<bool> AddStatistic(Statistic s);
        Task<MusicArtist> UpdateArtist(Artist a);
        Task<MusicSong> UpdateSong(Song s);
        Task<IEnumerable<bool?>> DeleteSong(List<int> songIds);
        Task<int> DeleteArtist(int artistId, bool noSongsLeft);       
        Task<SuggestedSong> AddSongSuggestion(SuggestedSong ss);
        Task<SuggestedSong> GetSongSuggestionById(int songSuggestionID);

    }
}
