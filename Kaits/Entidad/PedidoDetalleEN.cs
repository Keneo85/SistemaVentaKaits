using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class PedidoDetalleEN
    {
        [DisplayName("Nro Detalle Pedido")]
        public int Id_Pedido_Detalle { get; set; }

        [DisplayName("Nro Pedido")]
        public int Id_Pedido { get; set; }

        [DisplayName("Id_Producto")]
        public int Id_Producto { get; set; }

        [DisplayName("Producto")]
        public string Producto { get; set; }

        [DisplayName("Cantidad")]
        public int Cantidad { get; set; }

        [DisplayName("Precio")]
        public double Precio_Unitario { get; set; }

        [DisplayName("Sub total")]
        public double Subtotal { get; set; }

        [DisplayName("IGV")]
        public double IGV { get; set; }

    }
}
