using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Entidad;
using Microsoft.Ajax.Utilities;
using Negocio;

namespace AppKaits.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoBL pedidoBL = new PedidoBL();
        public ActionResult Index()
        {
            return View(pedidoBL.Listar());
        }

        public PedidoEN NroPedido()
        {
            var nroPedido = pedidoBL.Obtener_Numero_Pedido();
            PedidoEN en = new PedidoEN
            {
                Id_Pedido = nroPedido
            };
            return en;
        }

        public ActionResult Create()
        {
            var nroPedido = pedidoBL.Obtener_Numero_Pedido();
            ViewData["idpedido"] = nroPedido;
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public string ListarCliente()
        {
            ClienteBL clienteBL = new ClienteBL();
            var res = string.Empty;
            var lista = clienteBL.Listar();

            foreach (var item in lista)
            {
                var idcliente = item.Id_Cliente;
                var nombre = item.Nombre;
                var apellidopaterno = item.ApellidoPaterno;
                var apellidomaterno = item.ApellidoMaterno;
                var dni = item.DNI;
                var nomcom = nombre + " " + apellidopaterno + " " + apellidomaterno;

                res = res +
                    "<tr><td hidden=\"hidden\">" + idcliente + "</td>"
                    + "<td>" + nombre + "</td>"
                    + "<td>" + apellidopaterno + "</td>"
                    + "<td>" + apellidomaterno + "</td>"
                    + "<td>" + dni + "</td>"
                    + "<td class='text-center'>" + "<button type='button' class='btn btn-light selcliente' data-idcliente='" + idcliente + "' data-nombrecompleto='" + nomcom + "'>Seleccionar</button>" + "</td></tr>";
            }
            return res;
        }

        [HttpPost]
        public string ListarProducto()
        {
            ProductoBL productoBL = new ProductoBL();
            var res = string.Empty;
            var lista = productoBL.Listar();

            foreach (var item in lista)
            {
                var idproducto = item.Id_Producto;
                var descripcion = item.Descripcion;
                var precio = item.Precio;

                res = res +
                    "<tr><td hidden=\"hidden\">" + idproducto + "</td>"
                    + "<td>" + descripcion + "</td>"
                    + "<td>" + precio + "</td>"
                    + "<td class='text-center'>" + "<button type='button' class='btn btn-light selproducto' data-idproducto='" + idproducto + "' data-descripcion='" + descripcion + "' data-precio='" + precio + "'>Seleccionar</button>" + "</td></tr>";
            }
            return res;
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult Guardar(Array[] arraydetalle, double total_calculado, int idcliente, DateTime fechapedido, int idpedidocab)
        {
            bool respuesta = false;
            List<PedidoDetalleEN> listadetalle = new List<PedidoDetalleEN>();
            PedidoEN pedido = new PedidoEN();
            pedido.Id_Pedido = idpedidocab;
            pedido.Fecha_Pedido = fechapedido;
            pedido.Id_Cliente = idcliente;
            pedido.Total_Pedido = total_calculado;

            if (arraydetalle != null)
            {
                foreach (var item in arraydetalle)
                {
                    object idpedido = item.GetValue(0);
                    object idproducto = item.GetValue(1);
                    object producto = item.GetValue(2);
                    object cantidad = item.GetValue(3);
                    object precio = item.GetValue(4);
                    object subtot = item.GetValue(5);
                    object igv = item.GetValue(6);

                    PedidoDetalleEN detalle = new PedidoDetalleEN();
                    detalle.Id_Pedido = int.Parse(idpedido.ToString());
                    detalle.Id_Producto = int.Parse(idproducto.ToString());
                    detalle.Producto = producto.ToString();
                    detalle.Cantidad = int.Parse(cantidad.ToString());
                    detalle.Precio_Unitario = double.Parse(precio.ToString());
                    detalle.Subtotal = double.Parse(subtot.ToString());
                    detalle.IGV = double.Parse(igv.ToString());
                    listadetalle.Add(detalle);
                }
                respuesta = pedidoBL.Grabar(pedido, listadetalle);
            }
            return new JsonResult() { Data = respuesta, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult Eliminar(int idpedido)
        {
            bool resultado = pedidoBL.Eliminar(idpedido);
            return new JsonResult() { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
