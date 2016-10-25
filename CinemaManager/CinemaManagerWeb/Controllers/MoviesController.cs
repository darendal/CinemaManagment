using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CinemaManager;
using CinemaManager.DataAccess;
using AutoMapper.QueryableExtensions;
using CinemaManagerWeb.Models.DTO;

namespace CinemaManagerWeb.Controllers
{
    public class MoviesController : ApiController
    {
        private CinemaContext db = new CinemaContext();

        // GET: api/Movies
        public async Task<IList<MovieDTO>> GetMovies()
        {
            return await db.Movies.ProjectTo<MovieDTO>().ToListAsync();
        }

        // GET: api/Movies/5
        [ResponseType(typeof(MovieDTO))]
        public async Task<IHttpActionResult> GetMovie(int id)
        {
            MovieDTO movie = await db.Movies.ProjectTo<MovieDTO>().SingleOrDefaultAsync(m=>m.MovieId ==id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMovie(int id, MovieDTO movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.MovieId)
            {
                return BadRequest();
            }
            Movie MovieEntity = AutoMapper.Mapper.Map<Movie>(movie);
            db.Entry(MovieEntity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movies
        [ResponseType(typeof(MovieDTO))]
        public async Task<IHttpActionResult> PostMovie(MovieDTO movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Movie MovieEntity = AutoMapper.Mapper.Map<Movie>(movie);
            db.Movies.Add(MovieEntity);
            await db.SaveChangesAsync();

            movie = AutoMapper.Mapper.Map<MovieDTO>(MovieEntity);

            return CreatedAtRoute("DefaultApi", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> DeleteMovie(int id)
        {
            Movie movie = await db.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            await db.SaveChangesAsync();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Any(m=>m.MovieId == id);
        }
    }
}