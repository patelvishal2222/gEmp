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
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmpApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Bemp bemp = new Bemp();
        // GET api/EmpApi
        public List<Emp> GetEmps()
        {

            return bemp.ToList();
        }

        // GET api/EmpApi/5
        [ResponseType(typeof(Emp))]
        public IHttpActionResult GetEmp(int id)
        {
            Emp emp = db.Emps.Find(id);
            if (emp == null)
            {
                return NotFound();
            }

            return Ok(emp);
        }

        // PUT api/EmpApi/5
        public IHttpActionResult PutEmp(int id, Emp emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emp.EmpId)
            {
                return BadRequest();
            }

            db.Entry(emp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpExists(id))
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

        // POST api/EmpApi
        [ResponseType(typeof(Emp))]
        public IHttpActionResult PostEmp(Emp emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bemp.insertUpdate(emp);
            

            return CreatedAtRoute("DefaultApi", new { id = emp.EmpId }, emp);
        }

        // DELETE api/EmpApi/5
        [ResponseType(typeof(Emp))]
        public IHttpActionResult DeleteEmp(int id)
        {
            Emp emp = db.Emps.Find(id);
            if (emp == null)
            {
                return NotFound();
            }

            db.Emps.Remove(emp);
            db.SaveChanges();

            return Ok(emp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpExists(int id)
        {
            return db.Emps.Count(e => e.EmpId == id) > 0;
        }
    }
}