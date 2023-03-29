using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProductoEN
    {
        [DisplayName("Codigo")]
        public int Id_Producto { get; set; }
               
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }

        [DisplayName("Precio")]
        public double Precio { get; set; }
    }
}
