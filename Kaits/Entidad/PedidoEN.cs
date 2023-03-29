using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class PedidoEN
    {
        [DisplayName("Nro Pedido")]
        public int Id_Pedido { get; set; }

        [DisplayName("Id_Cliente")]
        public int Id_Cliente { get; set; }

        [DisplayName("Cliente")]
        public string Cliente { get; set; }


        [DisplayName("Fecha")]
        //[DisplayFormat(DataFormatString = "{0:d}")]
        //[DisplayFormat(DataFormatString = "{0:{0:HH:mm:ss dd/MM/yyyy}}", ApplyFormatInEditMode = true)]
        
        
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Fecha_Pedido { get; set; }

        [DisplayName("Total")]
        public double Total_Pedido { get; set; }

        public List<PedidoDetealleEN> listaPedidoDetalle = new List<PedidoDetealleEN>();
    }

    public class PedidoDetealleEN
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
        public double Cantidad { get; set; }

        [DisplayName("Precio")]
        public double Precio_Unitario { get; set; }

        [DisplayName("Sub total")]
        public double Subtotal { get; set; }

        [DisplayName("IGV")]
        public double IGV { get; set; }
    }
}
