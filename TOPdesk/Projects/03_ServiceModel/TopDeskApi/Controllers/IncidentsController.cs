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
using TOPdesk.Context;

namespace TopDeskApi.Controllers
{
    public class IncidentsController : ApiController
    {
        private TopDesk577Entities db = new TopDesk577Entities();

        // GET: api/Incidents
        public IQueryable<incident> Getincidents()
        {
            return db.incidents;
        }

        // GET: api/Incidents/5
        [ResponseType(typeof(incident))]
        public IHttpActionResult Getincident(Guid id)
        {
            incident incident = db.incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }

            return Ok(incident);
        }

        // PUT: api/Incidents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putincident(Guid id, incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != incident.unid)
            {
                return BadRequest();
            }

            db.Entry(incident).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!incidentExists(id))
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

        // POST: api/Incidents
        [ResponseType(typeof(incident))]
        public IHttpActionResult Postincident(incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.incidents.Add(incident);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (incidentExists(incident.unid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = incident.unid }, incident);
        }

        // DELETE: api/Incidents/5
        [ResponseType(typeof(incident))]
        public IHttpActionResult Deleteincident(Guid id)
        {
            incident incident = db.incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }

            db.incidents.Remove(incident);
            db.SaveChanges();

            return Ok(incident);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool incidentExists(Guid id)
        {
            return db.incidents.Count(e => e.unid == id) > 0;
        }
    }
}