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
using AutoMapper.QueryableExtensions;
using CinemaManager;
using CinemaManager.DataAccess;
using CinemaManagerWeb.Models.DTO;

namespace CinemaManagerWeb.Controllers
{
    public class MovieLeasesController : ApiController
    {
        private CinemaContext db = new CinemaContext();

        // GET: api/MovieLeases
        public async Task<IList<MovieLeaseDTO>> GetMovieLeases()
        {
            return await db.MovieLeases.ProjectTo<MovieLeaseDTO>().ToListAsync();
        }

        // GET: api/MovieLeases/5
        [ResponseType(typeof(MovieLeaseDTO))]
        public async Task<IHttpActionResult> GetMovieLease(int id)
        {
            MovieLeaseDTO movieLease = await db.MovieLeases.ProjectTo<MovieLeaseDTO>().SingleOrDefaultAsync(ml => ml.ID == id);
            if (movieLease == null)
            {
                return NotFound();
            }

            return Ok(movieLease);
        }

        // PUT: api/MovieLeases/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMovieLease(int id, MovieLeaseDTO movieLease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movieLease.ID)
            {
                return BadRequest();
            }

            MovieLease movieLeaseEntity = AutoMapper.Mapper.Map<MovieLease>(movieLease);
            db.Entry(movieLeaseEntity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieLeaseExists(id))
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

        // POST: api/MovieLeases
        [ResponseType(typeof(MovieLeaseDTO))]
        public async Task<IHttpActionResult> PostMovieLease(MovieLeaseDTO movieLease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MovieLease movieLeaseEntity = AutoMapper.Mapper.Map<MovieLease>(movieLease);
            db.MovieLeases.Add(movieLeaseEntity);
            await db.SaveChangesAsync();

            movieLease = AutoMapper.Mapper.Map<MovieLeaseDTO>(movieLeaseEntity);

            return CreatedAtRoute("DefaultApi", new { id = movieLease.ID }, movieLease);
        }

        // DELETE: api/MovieLeases/5
        [ResponseType(typeof(MovieLeaseDTO))]
        public async Task<IHttpActionResult> DeleteMovieLease(int id)
        {
            MovieLease movieLease = await db.MovieLeases.FindAsync(id);
            if (movieLease == null)
            {
                return NotFound();
            }

            db.MovieLeases.Remove(movieLease);
            await db.SaveChangesAsync();

            return Ok(AutoMapper.Mapper.Map<MovieLeaseDTO>(movieLease));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieLeaseExists(int id)
        {
            return db.MovieLeases.Any();
        }
    }
}