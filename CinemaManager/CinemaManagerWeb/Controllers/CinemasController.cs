using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CinemaManager;
using CinemaManager.DataAccess;
using CinemaManagerWeb.Models.DTO;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;

namespace CinemaManagerWeb.Controllers
{
    public class CinemasController : ApiController
    {
        private CinemaContext db = new CinemaContext();

        // GET: api/Cinemas
        public async Task<IList<CinemaDTO>> GetCinemas()
        {
            return await db.Cinemas.ProjectTo<CinemaDTO>().ToListAsync();
        }

        // GET: api/Cinemas/5
        [ResponseType(typeof(CinemaDTO))]
        public async Task<IHttpActionResult> GetCinema(int id)
        {
            CinemaDTO cinema = await db.Cinemas.ProjectTo<CinemaDTO>().SingleOrDefaultAsync(c=>c.CinemaId == id);
            if (cinema == null)
            {
                return NotFound();
            }

            return Ok(cinema);
        }

        // PUT: api/Cinemas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCinema(CinemaDTO cinema)
        {
            int id = cinema.CinemaId;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cinema CinemaEntity = AutoMapper.Mapper.Map<Cinema>(cinema);
            try
            {
                CinemaEntity.Validate();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            db.Entry(CinemaEntity).State = EntityState.Modified;

            try
            {
               await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CinemaExists(id))
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
        [ResponseType(typeof(CinemaDTO))]
        public async Task<IHttpActionResult> PostCinema(CinemaDTO cinema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cinema CinemaEntity = AutoMapper.Mapper.Map<Cinema>(cinema);
           
            try
            {
                CinemaEntity.Validate();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            db.Cinemas.Add(CinemaEntity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cinema.CinemaId }, cinema);
        }

        // DELETE: api/Cinemas/5
        [ResponseType(typeof(CinemaDTO))]
        public IHttpActionResult DeleteCinema(int id)
        {
            Cinema cinema = db.Cinemas.Find(id);
            if (cinema == null)
            {
                return NotFound();
            }

            db.Cinemas.Remove(cinema);
            db.SaveChanges();

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

        private async Task<bool> CinemaExists(int id)
        {
            return  await db.Cinemas.AnyAsync(e => e.CinemaId == id);
        }
    }
}