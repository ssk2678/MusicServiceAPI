using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicDataModels;
using MusicDataAccess;

namespace MusicServiceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MusicInformationController : ControllerBase
    {
        private IMusicDataRepository repo;
        // constructor injects repository registered in Startup
        public MusicInformationController(IMusicDataRepository repo)
        {
            this.repo = repo;
        }

        // GET: /MusicInformation/
        // GET: /MusicInformation/GetArtistById
        //this returns a single artist
        [HttpGet("/GetArtistById/{id}", Name = nameof(GetArtistById))] // named route
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetArtistById(int id)
        {
            Artist a = await repo.GetArtistById(id);
            if (a == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return Ok(a);   // 200 OK with artist in body
        }

        // GET: /MusicInformation/
        // GET: /MusicInformation/GetSongById
        //this returns a single song
        [HttpGet("/GetSongById/{id}", Name = nameof(GetSongById))] // named route
        [ProducesResponseType(200, Type = typeof(Song))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSongById(int id)
        {
            Song s = await repo.GetSongById(id);
            if (s == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return Ok(s);   // 200 OK with song in body
        }

        // GET: /MusicInformation/
        // GET: /MusicInformation/GetArtistsByDecade/[id]
        //this returns a selection of artists for a given decade
        [HttpGet("/GetArtistsByDecade/{decadeId}", Name = nameof(GetArtistsByDecade))] // named route
        [ProducesResponseType(200, 
            Type = typeof(IEnumerable<Artist>))]
        public async Task<IEnumerable<Artist>> GetArtistsByDecade(int decadeId)
        {
            //if (id != default)
                return await repo.GetArtistsByDecade(decadeId);
            //else
                //return await repo.GetArtistsByDecade((int)Decades.Eighties);
        }

        // GET: /MusicInformation
        // GET: MusicInformation/GetSongsByArtist/[id]
        //this returns songs by particular artist
        [HttpGet("/GetSongsByArtist/{artistId}", Name = nameof(GetSongsByArtist))] // named route
        [ProducesResponseType(200,
            Type = typeof(IEnumerable<Song>))]
        public async Task<IEnumerable<Song>> GetSongsByArtist(int artistId)
        {
            //if (id != default)
                return await repo.GetSongsByArtist(artistId);
            
        }

        // GET: /MusicInformation/
        // GET: /MusicInformation/GetSongsByArtistAndDecade
        //this returns songs by a particular artist in specific decade
        [HttpGet("/GetSongsByArtistAndDecade/{artistId}", Name = nameof(GetSongsByArtistAndDecade))] // named route
        [ProducesResponseType(200,
            Type = typeof(IEnumerable<Song>))]
        public async Task<IEnumerable<Song>> GetSongsByArtistAndDecade(int artistId, [FromQuery]int decadeId)
        {
            //if (id != default)
            return await repo.GetSongsByArtistAndDecade(artistId, decadeId);

        }


        // GET: /MusicInformation/
        // GET: /MusicInformation/GetDecades
        //this returns list of decades
        [HttpGet("/GetDecades", Name = nameof(GetDecades))] // named route
        [ProducesResponseType(200,
            Type = typeof(IEnumerable<Decade>))]
        public async Task<IEnumerable<Decade>> GetDecades()
        {
            return await repo.GetDecades();

        }

        // POST: /MusicInformation/
        // POST: /MusicInformation/AddArtist
        // BODY: Artist (JSON, XML)
        //this adds a new artist
        [HttpPost("/AddArtist", Name = nameof(AddArtist))]
        [ProducesResponseType(201, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddArtist([FromBody] Artist a)
        {
            if (a == null)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            Artist added = await repo.AddArtist(a);

            return CreatedAtRoute( // 201 Created
                routeName: nameof(GetArtistById),
                routeValues: new { id = added.ArtistID },
                value: added);

        }


        //// POST: /MusicInformation/
        //// POST: /MusicInformation/AddSong
        //// BODY: Song (JSON, XML)
        ////this adds a new song
        [HttpPost("/AddSong", Name = nameof(AddSong))]
        [ProducesResponseType(201, Type = typeof(Song))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddSong([FromBody] Song s)
        {
            if (s == null)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 BadRequest
            }

            Song added = await repo.AddSong(s);

            return CreatedAtRoute( // 201 Created
                routeName: nameof(GetSongById),
                routeValues: new { id = added.SongID },
                value: added);
        }

        // PUT: /MusicInformation/
        // PUT: /MusicInformation/UpdateArtist
        // BODY: Artist (JSON, XML)
        //this updates an existing artist
        [HttpPut("/UpdateArtist/{id}", Name = nameof(UpdateArtist))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateArtist(int id, [FromBody] Artist a)
        {

            if (a == null || a.ArtistID != id)
            {
                return BadRequest();    // 400 Bad request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // 400 Bad request
            }

            var existing = await repo.GetArtistById(id);

            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            a.DateAdded = existing.DateAdded;
            await repo.UpdateArtist(a);

            return new NoContentResult(); // 204 No content
        }

        //// PUT: /MusicInformation/
        //// PUT: /MusicInformation/UpdateSong
        //// BODY: Song (JSON, XML)
        ////this updates an existing song
        [HttpPut("/UpdateSong/{id}", Name = nameof(UpdateSong))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> UpdateSong(int id, [FromBody] Song s)
        {
            if(s == null || s.SongID != id)
            {
                return BadRequest(); // 400 Bad Request
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // 400 Bad Request
            }

            var existing = await repo.GetSongById(id);

            if (existing == null)
            {
                return NotFound();  // 404 Resource not found
            }

            s.DateAdded = existing.DateAdded;
            await repo.UpdateSong(s);

            return new NoContentResult(); // 204 No content
        }

        // GET: /MusicInformation/
        // GET: /MusicInformation/GetSuggestedSongById
        // this returns a single suggested song
        [HttpGet("/GetSuggestedSongById/{id}", Name = nameof(GetSuggestedSongById))] // named route
        [ProducesResponseType(200, Type = typeof(SuggestedSong))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSuggestedSongById(int id)
        {
            SuggestedSong s = await repo.GetSongSuggestionById(id);
            if (s == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return Ok(s);   // 200 OK with song in body
        }

        //// POST:  /MusicInformation/AddSuggestedSong
        //// BODY:  SuggestedSong (JSON, XML)
        //// this inserts a song suggestion
        [HttpPost("/AddSuggestedSong", Name = nameof(AddSuggestedSong))]
        [ProducesResponseType(201, Type = typeof(SuggestedSong))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> AddSuggestedSong([FromBody] SuggestedSong ss)
        {
            if (ss == null)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 BadRequest
            }

            SuggestedSong suggestedSong = await repo.AddSongSuggestion(ss);

            return  // 201 Created
               CreatedAtRoute(
                    routeName: nameof(GetSuggestedSongById),
                    routeValues: new { id = suggestedSong.SuggestedSongId },
                    value: suggestedSong);
        }
    }
}
