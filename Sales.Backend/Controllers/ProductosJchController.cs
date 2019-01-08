using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales.Backend.Models;
using Sales.Common.Models;

namespace Sales.Backend.Controllers
{
    public class ProductosJchController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ProductosJch
        public async Task<ActionResult> Index()
        {
            return View(await db.TBM_PRODU_JCH.ToListAsync());
        }

        // GET: ProductosJch/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBM_PRODU_JCH tBM_PRODU_JCH = await db.TBM_PRODU_JCH.FindAsync(id);
            if (tBM_PRODU_JCH == null)
            {
                return HttpNotFound();
            }
            return View(tBM_PRODU_JCH);
        }

        // GET: ProductosJch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductosJch/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "COD_PRODU,DES_PRODU,COD_USUAR_CREAC,FEC_USUAR_CREAC,COD_USUAR_MODIF,FEC_USUAR_MODIF")] TBM_PRODU_JCH tBM_PRODU_JCH)
        {
            if (ModelState.IsValid)
            {
                db.TBM_PRODU_JCH.Add(tBM_PRODU_JCH);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tBM_PRODU_JCH);
        }

        // GET: ProductosJch/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBM_PRODU_JCH tBM_PRODU_JCH = await db.TBM_PRODU_JCH.FindAsync(id);
            if (tBM_PRODU_JCH == null)
            {
                return HttpNotFound();
            }
            return View(tBM_PRODU_JCH);
        }

        // POST: ProductosJch/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "COD_PRODU,DES_PRODU,COD_USUAR_CREAC,FEC_USUAR_CREAC,COD_USUAR_MODIF,FEC_USUAR_MODIF")] TBM_PRODU_JCH tBM_PRODU_JCH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBM_PRODU_JCH).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tBM_PRODU_JCH);
        }

        // GET: ProductosJch/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBM_PRODU_JCH tBM_PRODU_JCH = await db.TBM_PRODU_JCH.FindAsync(id);
            if (tBM_PRODU_JCH == null)
            {
                return HttpNotFound();
            }
            return View(tBM_PRODU_JCH);
        }

        // POST: ProductosJch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TBM_PRODU_JCH tBM_PRODU_JCH = await db.TBM_PRODU_JCH.FindAsync(id);
            db.TBM_PRODU_JCH.Remove(tBM_PRODU_JCH);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
