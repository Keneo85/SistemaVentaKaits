using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidad;
using Negocio;

namespace AppKaits.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoBL productoBL = new ProductoBL();
        public ActionResult Index()
        {
            return View(productoBL.Listar());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ProductoEN en = new ProductoEN();
                en.Descripcion = collection["Descripcion"];
                en.Precio = Convert.ToDouble(collection["Precio"]);
                productoBL.Grabar(en);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id = 0)
        {
            var producto = productoBL.Obtener(id);
            return View(producto);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                ProductoEN en = new ProductoEN();
                en.Id_Producto = id;
                en.Descripcion = collection["Descripcion"];
                en.Precio = Convert.ToDouble(collection["Precio"]);
                productoBL.Actualizar(en);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var producto = productoBL.Obtener(id);
            return View(producto);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var resultado = productoBL.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult Eliminar(int idproducto)
        {
            bool resultado = productoBL.Eliminar(idproducto);
            return new JsonResult() { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult Editar(int idproducto, string descpro, double prep)
        {
            ProductoEN producto = new ProductoEN();
            producto.Id_Producto = idproducto;
            producto.Descripcion = descpro;
            producto.Precio = prep;
            bool resultado = productoBL.Actualizar(producto);
            return new JsonResult() { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult Grabar(string descpro, double prep)
        {
            ProductoEN producto = new ProductoEN();
            producto.Descripcion = descpro;
            producto.Precio = prep;
            bool resultado = productoBL.Grabar(producto);
            return new JsonResult() { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
