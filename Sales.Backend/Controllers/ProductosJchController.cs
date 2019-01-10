
namespace Sales.Backend.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Backend.Models;
    using Common.Models;
    using System.Linq;
    using Sales.Backend.Helpers;
    using System;

    public class ProductosJchController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ProductosJch
        public async Task<ActionResult> Index() // ACCION POR DEFECTO
        {
            //                                      uso de LinQ
            return View(await this.db.TBM_PRODU_JCH.OrderBy(p => p.DES_PRODU).ToListAsync());
        }

        // GET: ProductosJch/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBM_PRODU_JCH tBM_PRODU_JCH = await this.db.TBM_PRODU_JCH.FindAsync(id);
            if (tBM_PRODU_JCH == null)
            {
                return HttpNotFound();
            }
            return View(tBM_PRODU_JCH);
        }

        // GET: ProductosJch/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductosJch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductosJchView productosJchView)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/ProductosJchImg";

                if (productosJchView.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(productosJchView.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                // creamos un nuevo productoJch (TBM_PROD_JCH) , que sera el resultado del metodo
                var productosJch = this.ToProductosJch(productosJchView,pic); 

                this.db.TBM_PRODU_JCH.Add(productosJch);
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productosJchView);
        }

        //METODO PARA CONVERTIR LA IMAGEN A UN STRING
        //SE IGUALAN LOS CAMPOS DEL OBJ TBM_PRODU_JCH A DEL OBJ VIEW QUE CONTIENE LA IMG SELECCIONADA POR EL USER
        private TBM_PRODU_JCH ToProductosJch(ProductosJchView productosJchView, string pic )
        {
            return new TBM_PRODU_JCH
            {
                COD_PRODU = productosJchView.COD_PRODU,
                DES_PRODU = productosJchView.DES_PRODU,
                Remarks = productosJchView.Remarks,
                ImagePath = pic,            // imagen capturada
                COD_USUAR_CREAC = productosJchView.COD_USUAR_CREAC,
                FEC_USUAR_CREAC = productosJchView.FEC_USUAR_CREAC,
                COD_USUAR_MODIF = productosJchView.COD_USUAR_MODIF,
                FEC_USUAR_MODIF = productosJchView.FEC_USUAR_MODIF,
            };
        }

        // GET: ProductosJch/Edit/5
        public async Task<ActionResult> Edit(int? id)//int? = la variable puede llegar nula
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tBM_PRODU_JCH = await this.db.TBM_PRODU_JCH.FindAsync(id);

            if (tBM_PRODU_JCH == null)
            {
                return HttpNotFound();
            }

            var productosJchView = this.ToProductosJchView(tBM_PRODU_JCH);

            return View(productosJchView);
        }

        private ProductosJchView ToProductosJchView(TBM_PRODU_JCH tBM_PRODU_JCH)
        {
            return new ProductosJchView
            {
                COD_PRODU = tBM_PRODU_JCH.COD_PRODU,
                DES_PRODU = tBM_PRODU_JCH.DES_PRODU,
                Remarks = tBM_PRODU_JCH.Remarks,
                ImagePath = tBM_PRODU_JCH.ImagePath,            // imagen capturada
                COD_USUAR_CREAC = tBM_PRODU_JCH.COD_USUAR_CREAC,
                FEC_USUAR_CREAC = tBM_PRODU_JCH.FEC_USUAR_CREAC,
                COD_USUAR_MODIF = tBM_PRODU_JCH.COD_USUAR_MODIF,
                FEC_USUAR_MODIF = tBM_PRODU_JCH.FEC_USUAR_MODIF,
            };
        }

        // POST: ProductosJch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductosJchView productosJchView)
        {
            if (ModelState.IsValid)
            {
                var pic = productosJchView.ImagePath;
                var folder = "~/Content/ProductosJchImg";

                if (productosJchView.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(productosJchView.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                // creamos un nuevo productoJch (TBM_PROD_JCH) , que sera el resultado del metodo
                var productosJch = this.ToProductosJch(productosJchView, pic);

                this.db.Entry(productosJch).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productosJchView);
        }

        // GET: ProductosJch/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tBM_PRODU_JCH = await this.db.TBM_PRODU_JCH.FindAsync(id);

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
            var tBM_PRODU_JCH = await this.db.TBM_PRODU_JCH.FindAsync(id);
            this.db.TBM_PRODU_JCH.Remove(tBM_PRODU_JCH);
            await this.db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
