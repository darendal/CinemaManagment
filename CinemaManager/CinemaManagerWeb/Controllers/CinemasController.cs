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

namespace CinemaManagerWeb.Controllers
{
    public class CinemasController : ApiController
    {
        private CinemaContext db = new CinemaContext();

        // GET: api/Cinemas
        public IQueryable<Cinema> AllCinemas()
        {
            return db.Cinemas;
        }

        // GET: api/Cinemas/5
        [ResponseType(typeof(Cinema))]
        [AcceptVerbs("GET")]
        public async Task<IHttpActionResult> Cinema(int id)
        {
            Cinema cinema = await db.Cinemas.FindAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }

            return Ok(cinema);
        }

        // PUT: api/Cinemas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCinema(int id, Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cinema.CinemaId)
            {
                return BadRequest();
            }

            db.Entry(cinema).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaExists(id))
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

        // POST: api/Cinemas
        [ResponseType(typeof(Cinema))]
        public async Task<IHttpActionResult> PostCinema(Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cinemas.Add(cinema);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cinema.CinemaId }, cinema);
        }

        // DELETE: api/Cinemas/5
        [ResponseType(typeof(Cinema))]
        public async Task<IHttpActionResult> DeleteCinema(int id)
        {
            Cinema cinema = await db.Cinemas.FindAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }

            db.Cinemas.Remove(cinema);
            await db.SaveChangesAsync();

            return Ok(cinema);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CinemaExists(int id)
        {
            return db.Cinemas.Count(e => e.CinemaId == id) > 0;
        }
    }
}