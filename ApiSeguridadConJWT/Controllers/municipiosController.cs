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
    public class municipiosController : ApiController
    {
        private practica_seguridadEntities db = new practica_seguridadEntities();

        // GET: api/municipios
        [Authorize]
        public IQueryable<municipio> Getmunicipio()
        {
            return db.municipio;
        }

        // GET: api/municipios/5
        [Authorize]
        [ResponseType(typeof(municipio))]
        public IHttpActionResult Getmunicipio(int id)
        {
            municipio municipio = db.municipio.Find(id);
            if (municipio == null)
            {
                return NotFound();
            }

            return Ok(municipio);
        }

        // PUT: api/municipios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmunicipio(int id, municipio municipio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != municipio.id_municipio)
            {
                return BadRequest();
            }

            db.Entry(municipio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!municipioExists(id))
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

        // POST: api/municipios
        [ResponseType(typeof(municipio))]
        public IHttpActionResult Postmunicipio(municipio municipio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.municipio.Add(municipio);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (municipioExists(municipio.id_municipio))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = municipio.id_municipio }, municipio);
        }

        // DELETE: api/municipios/5
        [Authorize]
        [ResponseType(typeof(municipio))]
        public IHttpActionResult Deletemunicipio(int id)
        {
            municipio municipio = db.municipio.Find(id);
            if (municipio == null)
            {
                return NotFound();
            }

            db.municipio.Remove(municipio);
            db.SaveChanges();

            return Ok(municipio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool municipioExists(int id)
        {
            return db.municipio.Count(e => e.id_municipio == id) > 0;
        }
    }
}