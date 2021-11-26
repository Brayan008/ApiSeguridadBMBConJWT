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
    public class historial_pagosController : ApiController
    {
        private practica_seguridadEntities db = new practica_seguridadEntities();

        // GET: api/historial_pagos
        public IQueryable<historial_pagos> Gethistorial_pagos()
        {
            return db.historial_pagos;
        }

        // GET: api/historial_pagos/5
        [ResponseType(typeof(historial_pagos))]
        public IHttpActionResult Gethistorial_pagos(int id)
        {
            historial_pagos historial_pagos = db.historial_pagos.Find(id);
            if (historial_pagos == null)
            {
                return NotFound();
            }

            return Ok(historial_pagos);
        }

        // PUT: api/historial_pagos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puthistorial_pagos(int id, historial_pagos historial_pagos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != historial_pagos.id_historial)
            {
                return BadRequest();
            }

            db.Entry(historial_pagos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!historial_pagosExists(id))
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

        // POST: api/historial_pagos
        [ResponseType(typeof(historial_pagos))]
        public IHttpActionResult Posthistorial_pagos(historial_pagos historial_pagos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.historial_pagos.Add(historial_pagos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (historial_pagosExists(historial_pagos.id_historial))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = historial_pagos.id_historial }, historial_pagos);
        }

        // DELETE: api/historial_pagos/5
        [ResponseType(typeof(historial_pagos))]
        public IHttpActionResult Deletehistorial_pagos(int id)
        {
            historial_pagos historial_pagos = db.historial_pagos.Find(id);
            if (historial_pagos == null)
            {
                return NotFound();
            }

            db.historial_pagos.Remove(historial_pagos);
            db.SaveChanges();

            return Ok(historial_pagos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool historial_pagosExists(int id)
        {
            return db.historial_pagos.Count(e => e.id_historial == id) > 0;
        }
    }
}