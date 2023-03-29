using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Entidad;
using Negocio;
using Newtonsoft.Json;

namespace AppKaits.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteBL clienteBL = new ClienteBL();
        public ActionResult Index()
        {
            return View(clienteBL.Listar());
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
                ClienteEN en = new ClienteEN();
                en.Nombre = collection["Nombre"];
                en.ApellidoPaterno = collection["ApellidoPaterno"];
                en.ApellidoMaterno = collection["ApellidoMaterno"];
                en.DNI = collection["DNI"];
                clienteBL.Grabar(en);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id = 0)
        {
            var cliente = clienteBL.Obtener(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                ClienteEN en = new ClienteEN();
                en.Id_Cliente = id;
                en.Nombre = collection["Nombre"];
                en.ApellidoPaterno = collection["ApellidoPaterno"];
                en.ApellidoMaterno = collection["ApellidoMaterno"];
                en.DNI = collection["DNI"];
                clienteBL.Actualizar(en);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id = 0)
        {
            var cliente = clienteBL.Obtener(id);
            return View(cliente);
        }

        [HttpPost]
        public JsonResult Eliminar(int idcliente)
        {
            bool resultado = clienteBL.Eliminar(idcliente);
            return new JsonResult() { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult Editar(int idcliente, string nomcliente, string apcliente, string amcliente, string dni)
        {
            ClienteEN cliente = new ClienteEN();
            cliente.Id_Cliente = idcliente;
            cliente.Nombre = nomcliente;
            cliente.ApellidoPaterno = apcliente;
            cliente.ApellidoMaterno = amcliente;
            cliente.DNI = dni;
            bool resultado = clienteBL.Actualizar(cliente);
            return new JsonResult() { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult Grabar(string nomcliente, string apcliente, string amcliente, string dni)
        {
            ClienteEN cliente = new ClienteEN();
            cliente.Nombre = nomcliente;
            cliente.ApellidoPaterno = apcliente;
            cliente.ApellidoMaterno = amcliente;
            cliente.DNI = dni;
            bool resultado = clienteBL.Grabar(cliente);
            return new JsonResult() { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
