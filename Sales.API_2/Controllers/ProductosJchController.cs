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
using Sales.Common.Models;
using Sales.Domain.Models;

namespace Sales.API_2.Controllers
{
    public class ProductosJchController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ProductosJch
        public IQueryable<TBM_PRODU_JCH> GetTBM_PRODU_JCH()
        {
            return db.TBM_PRODU_JCH;
        }

        // GET: api/ProductosJch/5
        [ResponseType(typeof(TBM_PRODU_JCH))]
        public async Task<IHttpActionResult> GetTBM_PRODU_JCH(int id)
        {
            TBM_PRODU_JCH tBM_PRODU_JCH = await db.TBM_PRODU_JCH.FindAsync(id);
            if (tBM_PRODU_JCH == null)
            {
                return NotFound();
            }

            return Ok(tBM_PRODU_JCH);
        }

        // PUT: api/ProductosJch/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTBM_PRODU_JCH(int id, TBM_PRODU_JCH tBM_PRODU_JCH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tBM_PRODU_JCH.COD_PRODU)
            {
                return BadRequest();
            }

            db.Entry(tBM_PRODU_JCH).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TBM_PRODU_JCHExists(id))
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

        // POST: api/ProductosJch
        [ResponseType(typeof(TBM_PRODU_JCH))]
        public async Task<IHttpActionResult> PostTBM_PRODU_JCH(TBM_PRODU_JCH tBM_PRODU_JCH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TBM_PRODU_JCH.Add(tBM_PRODU_JCH);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tBM_PRODU_JCH.COD_PRODU }, tBM_PRODU_JCH);
        }

        // DELETE: api/ProductosJch/5
        [ResponseType(typeof(TBM_PRODU_JCH))]
        public async Task<IHttpActionResult> DeleteTBM_PRODU_JCH(int id)
        {
            TBM_PRODU_JCH tBM_PRODU_JCH = await db.TBM_PRODU_JCH.FindAsync(id);
            if (tBM_PRODU_JCH == null)
            {
                return NotFound();
            }

            db.TBM_PRODU_JCH.Remove(tBM_PRODU_JCH);
            await db.SaveChangesAsync();

            return Ok(tBM_PRODU_JCH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TBM_PRODU_JCHExists(int id)
        {
            return db.TBM_PRODU_JCH.Count(e => e.COD_PRODU == id) > 0;
        }
    }
}