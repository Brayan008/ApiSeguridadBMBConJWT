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
using ApiSeguridadConJWT.Models;

namespace ApiSeguridadConJWT.Controllers
{
    [Authorize]
    public class tarjetasController : ApiController
    {
        private practica_seguridadEntities db = new practica_seguridadEntities();

        // GET: api/tarjetas
        public IQueryable<tarjeta> Gettarjeta()
        {
            return db.tarjeta;
        }

        // GET: api/tarjetas/5
        [ResponseType(typeof(tarjeta))]
        public IHttpActionResult Gettarjeta(int id)
        {
            tarjeta tarjeta = db.tarjeta.Find(id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            return Ok(tarjeta);
        }

        // PUT: api/tarjetas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttarjeta(int id, tarjeta tarjeta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarjeta.id_tarjeta)
            {
                return BadRequest();
            }

            db.Entry(tarjeta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tarjetaExists(id))
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

        // POST: api/tarjetas
        [ResponseType(typeof(tarjeta))]
        public IHttpActionResult Posttarjeta(tarjeta tarjeta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tarjeta.Add(tarjeta);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tarjetaExists(tarjeta.id_tarjeta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tarjeta.id_tarjeta }, tarjeta);
        }

        // DELETE: api/tarjetas/5
        [ResponseType(typeof(tarjeta))]
        public IHttpActionResult Deletetarjeta(int id)
        {
            tarjeta tarjeta = db.tarjeta.Find(id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            db.tarjeta.Remove(tarjeta);
            db.SaveChanges();

            return Ok(tarjeta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tarjetaExists(int id)
        {
            return db.tarjeta.Count(e => e.id_tarjeta == id) > 0;
        }
    }
}