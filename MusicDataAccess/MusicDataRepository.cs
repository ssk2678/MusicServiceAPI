using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using MusicDataAccess.Models;
using MusicDataModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace MusicDataAccess
{
    public class MusicDataRepository : IMusicDataRepository
    {

        // use a static thread-safe dictionary field to cache the customers
        private static ConcurrentDictionary<int, MusicArtist> musicArtistsCache;
        private static ConcurrentDictionary<int, MusicSong> musicSongsCache;
        //private static ConcurrentDictionary<int, MusicStatistic> musicStatisticCache;


        private MusicContext db;

        public MusicDataRepository(MusicContext db)
        {
            this.db = db;

            // pre-load artists from database as a normal
            // Dictionary with ArtistID as the key
            // then convert to a thread-safe ConcurrentDictionary
            if (musicArtistsCache == null)
            {
                musicArtistsCache = new ConcurrentDictionary
                    <int, MusicArtist>(
                    db.MusicArtists.ToDictionary(ma => ma.ArtistId));

                musicSongsCache = new ConcurrentDictionary<int, MusicSong>(
                    db.MusicSongs.ToDictionary(ms => ms.SongId));

                //musicStatisticCache = new ConcurrentDictionary<int, MusicStatistic>(
                //    db.MusicStatistics.ToDictionary(mstat => mstat.MusicStatId));
            }
        }

        public Task<IEnumerable<Decade>> GetDecades()
        {
            
            Task<IEnumerable<Decade>> decades = Task.Run(() => db.MusicDecades.AsEnumerable().Select(dec =>
                new Decade
                {
                    DecadeID = dec.DecadeId,
                    DateAdded = dec.DateAdded,
                    DecadeName = dec.DecadeName,
                    DateModified = dec.DateModified

                }));


            return decades;

        }
        public Task<Artist> GetArtistById(int artistID)
        {
            Task<Artist> artist = Task.Run(() => musicArtistsCache.Where(mac => mac.Value.ArtistId == artistID).Select(ma => new Artist
            {
                ArtistID = ma.Value.ArtistId,
                ArtistName = ma.Value.ArtistName,
                DateAdded = ma.Value.DateAdded.Value,
                DateModified = ma.Value.DateModified.Value

            }).Single());

            return artist;
        }
        public Task<IEnumerable<Artist>> GetArtistsByDecade(int decadeID)
        {

            Task<IEnumerable<Artist>> artists = Task.Run(() => musicArtistsCache.Join(db.MusicArtistDecades,
                                                            artist => artist.Value.ArtistId,
                                                            artistDecade => artistDecade.ArtistId,
                                                            (artist, artistDecade) => ( new {
                                                                ArtistID = artist.Value.ArtistId,
                                                                ArtistName = artist.Value.ArtistName,                                                                
                                                                DecadeValue = artistDecade.DecadeId,
                                                                DateAdded = artist.Value.DateAdded,
                                                                DateModified = artist.Value.DateModified
                                                                

                                                            })).Where(artDecade => artDecade.DecadeValue == decadeID)
                                                            .AsEnumerable().Select(art =>
                                                                new Artist
                                                                {
                                                                    ArtistID = art.ArtistID,
                                                                    ArtistName = art.ArtistName,
                                                                    DateAdded = art.DateAdded.Value,
                                                                    DateModified = art.DateModified.Value

                                                                }));

            return artists;

        }

        public Task<Song> GetSongById(int songID)
        {
            Task<Song> song = Task.Run(() => musicSongsCache.Where(mac => mac.Value.SongId == songID).Select(ms => new Song
            {
             SongID = ms.Value.SongId,
             SongArtistID = ms.Value.SongArtistId.Value,
             SongDecadeID = ms.Value.SongDecadeId.Value,
             SongName = ms.Value.SongName,
             DateAdded = ms.Value.DateAdded.Value,
             DateModified = ms.Value.DateModified.Value

            }).Single());

            return song;
        }

        public Task<IEnumerable<Song>> GetSongsByArtist(int artistID)
        {

            Task<IEnumerable<Song>> songs = Task.Run(() => musicSongsCache.Where(ms => ms.Value.SongArtistId == artistID).Select(song =>
                                                        new Song
                                                        {
                                                            SongID = song.Value.SongId,
                                                            SongName = song.Value.SongName

                                                        }));

            return songs;

        }

        public Task<IEnumerable<Song>> GetSongsByArtistAndDecade(int artistID, int decadeID)
        {
            Task<IEnumerable<Song>> songs = Task.Run(() => musicSongsCache.Where(ms => ms.Value.SongArtistId == artistID && ms.Value.SongDecadeId == decadeID).Select(song =>
                                                        new Song
                                                        {
                                                            SongID = song.Value.SongId,
                                                            SongName = song.Value.SongName

                                                        }));

            return songs;
        }

        private MusicArtist UpdateArtistCache(int id, MusicArtist ma)
        {
            MusicArtist old;
            if (musicArtistsCache.TryGetValue(id, out old))
            {
                if (musicArtistsCache.TryUpdate(id, ma, old))
                {
                    return ma;
                }
            }
            return null;
        }

        private MusicSong UpdateSongCache(int id, MusicSong ms)
        {
            MusicSong old;
            if (musicSongsCache.TryGetValue(id, out old))
            {
                if (musicSongsCache.TryUpdate(id, ms, old))
                {
                    return ms;
                }
            }
            return null;
        }       

        public async Task<Artist> AddArtist(Artist a)
        {
            MusicArtist newMusicArtist = new MusicArtist
            {
                ArtistName = a.ArtistName,
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            };

            // mark artist as added in change tracking
            EntityEntry<MusicArtist> added = await db.MusicArtists.AddAsync(newMusicArtist);

            // save tracked change to database
            int affected = await db.SaveChangesAsync();

            if (affected >= 1)
            {
                // if the artist is new, add it to cache, else
                // call UpdateArtistCache method
                newMusicArtist.ArtistId = added.Entity.ArtistId;
                MusicArtist ma = musicArtistsCache.AddOrUpdate(newMusicArtist.ArtistId, newMusicArtist, UpdateArtistCache);
                Artist newArtist = new Artist { ArtistID = ma.ArtistId, ArtistName = ma.ArtistName, DateAdded = ma.DateAdded.Value, DateModified = ma.DateModified.Value };
                return newArtist;
            }
            else
            {
                return null;
            }
        }

        public async Task<Song> AddSong(Song s)
        {
            
            MusicSong newMusicSong = new MusicSong
            {
                SongName = s.SongName,
                SongArtistId = s.SongArtistID,
                SongDecadeId = s.SongDecadeID,
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now

            };

            // mark song as added in change tracking
            EntityEntry<MusicSong> added = await db.MusicSongs.AddAsync(newMusicSong);

            // save tracked change to database
            int affected = await db.SaveChangesAsync();

            if (affected >= 1)
            {
                // if the song is new, add it to cache, else
                // call UpdateSongCache method
                newMusicSong.SongId = added.Entity.SongId;
                MusicSong ms =  musicSongsCache.AddOrUpdate(newMusicSong.SongId, newMusicSong, UpdateSongCache);
                Song newSong = new Song { SongID = ms.SongId, SongArtistID = ms.SongArtistId.Value, SongName = ms.SongName, SongDecadeID = ms.SongDecadeId.Value, DateAdded = ms.DateAdded.Value, DateModified = ms.DateModified.Value  };
                return newSong;

            }
            else
            {
                return null;
            }
            
        }      

        public async Task<MusicArtist> UpdateArtist(Artist a)
        {
            // get Artist to be updated
            //MusicArtist ma = musicArtistsCache.Single(ma => ma.Value.ArtistId == a.ArtistID).Value;
            MusicArtist ma = new MusicArtist
            {
                ArtistId = a.ArtistID,
                ArtistName = a.ArtistName,
                DateAdded = a.DateAdded,
                DateModified = DateTime.Now
            };
                        
            db.MusicArtists.Update(ma);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                // updated in cache
                return UpdateArtistCache(ma.ArtistId, ma);
            }

            return null;
        }

        public async Task<MusicSong> UpdateSong(Song s)
        {

            // get Song to be updated
            //MusicSong ms = musicSongsCache.Single(ms => ms.Value.SongId == s.SongID).Value;
            MusicSong ms = new MusicSong
            {
                SongId = s.SongID,
                SongName = s.SongName,
                SongArtistId = s.SongArtistID,
                SongDecadeId = s.SongDecadeID,
                DateAdded = s.DateAdded,
                DateModified = DateTime.Now
            };            
                       
            db.MusicSongs.Update(ms);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                // update in cache
                return UpdateSongCache(ms.SongId, ms);
            }

            return null;

        }

        public async Task<IEnumerable<bool?>> DeleteSong(List<int> songIds) //Need to consider - check if last song within decade and delete artist from MusicArtist table and/or check if no other decade for that artist and delete artist from Artist table?
        {
            List<bool?> deleteResultList = new List<bool?>();

            //delete any music statistic associated with song
            IEnumerable<MusicClickStatistic> msStatisticList = db.MusicClickStatistics.Where(ms => songIds.Contains(ms.MusicClickStatSongId));
            db.MusicClickStatistics.RemoveRange(msStatisticList);

            //get and delete song
            List<MusicSong> msSongList = db.MusicSongs.Where(ms => songIds.Contains(ms.SongId)).ToList();

            List<int?> artistIds = new();
            foreach(MusicSong msSong in msSongList)
            {
                artistIds.Add(msSong.SongArtistId);
            }

            // remove from database
            db.MusicSongs.RemoveRange(msSongList);

            int affected = await db.SaveChangesAsync();

            // delete the artist if that was his/her/their last song
            foreach (int artistId in artistIds)
            {
                bool stillHasSongs = db.MusicSongs.Where(ms => ms.SongArtistId == artistId).Any();

                if (!stillHasSongs)
                {
                    int deleteArtistTask = await DeleteArtist(artistId, true); //need to modify delete artist method to accomodate being called from here - no need to check song table
                }
            }

            if (affected == 1) //?? Should this be >=
            {
                // remove from cache
                songIds.ForEach(si =>
                {
                    deleteResultList.Add(musicSongsCache.TryRemove(si, out MusicSong ms));

                });
                return deleteResultList;
            }
            else
            {
                return null;
            }
           
        }

        public async Task<int> DeleteArtist(int artistId, bool noSongsLeft = false)
        {
            if (!noSongsLeft)
            {
                // delete any music statistic associated with artist
                IEnumerable<MusicClickStatistic> mstatList = db.MusicClickStatistics.Where(ms => ms.MusicClickStatArtistId == artistId);
                db.MusicClickStatistics.RemoveRange(mstatList);

                // delete any entries from the music song table associated with artist
                IEnumerable<MusicSong> msList = db.MusicSongs.Where(ms => ms.SongArtistId == artistId);
                db.MusicSongs.RemoveRange(msList);
            }            

            // delete artist from the artist table
            MusicArtist ma = db.MusicArtists.Single(ma => ma.ArtistId == artistId);
            db.MusicArtists.Remove(ma);

            int affected = await db.SaveChangesAsync();
            return affected;

        }

        public async Task<SuggestedSong> AddSongSuggestion(MusicDataModels.SuggestedSong ss)
        {
            MusicDataAccess.Models.SongSuggestion newMusicSongSuggestion = new MusicDataAccess.Models.SongSuggestion
            {
                SuggestedSongId = ss.SuggestedSongId,
                SuggestedSongArtist = ss.SuggestedSongArtist,
                SuggestedSongTitle = ss.SuggestedSongTitle,
                SuggestedSongDecade = ss.SuggestedSongDecade
            };

            // mark song as added in change tracking
            EntityEntry<MusicDataAccess.Models.SongSuggestion> added = await db.SongSuggestions.AddAsync(newMusicSongSuggestion);

            int affected = await db.SaveChangesAsync();

            if (affected >= 1)
            {
                // if the song is new, add it to cache, else
                // call UpdateSongCache method
                newMusicSongSuggestion.SuggestedSongId = added.Entity.SuggestedSongId;
                
                SuggestedSong newSongSuggestion = new SuggestedSong{ SuggestedSongId = newMusicSongSuggestion.SuggestedSongId, 
                                                                       SuggestedSongArtist = newMusicSongSuggestion.SuggestedSongArtist, 
                                                                       SuggestedSongDecade = newMusicSongSuggestion.SuggestedSongDecade, 
                                                                       SuggestedSongTitle = newMusicSongSuggestion.SuggestedSongTitle };
                return newSongSuggestion;

            }
            else
            {
                return null;
            }

        }

        public Task<SuggestedSong> GetSongSuggestionById(int songSuggestionID)
        {
            Task<SuggestedSong> songSuggestion = Task.Run(() => db.SongSuggestions.Where(mss => mss.SuggestedSongId == songSuggestionID).Select(ma => new SuggestedSong
            {
                SuggestedSongId = ma.SuggestedSongId,
                SuggestedSongArtist = ma.SuggestedSongArtist,
                SuggestedSongDecade = ma.SuggestedSongDecade,
                SuggestedSongTitle = ma.SuggestedSongTitle

            }).Single());

            return songSuggestion;
        }
    }
}
