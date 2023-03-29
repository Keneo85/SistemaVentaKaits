using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ClienteEN
    {
        [DisplayName("Codigo")]
        public int Id_Cliente { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [StringLength(8, ErrorMessage = "DNI incorrecto")]
        [DisplayName("DNI")]
        public string DNI { get; set; }
    }
}
