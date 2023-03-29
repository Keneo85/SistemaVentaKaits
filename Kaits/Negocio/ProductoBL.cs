using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoBL
    {
        private readonly ProductoDA productoDA = new ProductoDA();

        public List<ProductoEN> Listar()
        {
            return productoDA.Listar();
        }

        public ProductoEN Obtener(int idproducto)
        {
            return productoDA.Obtener(idproducto);
        }

        public ProductoEN Buscar(string descripcion)
        {
            return productoDA.Buscar(descripcion);
        }

        public bool Actualizar(ProductoEN cliente)
        {
            return productoDA.Actualizar(cliente);
        }

        public bool Grabar(ProductoEN cliente)
        {
            return productoDA.Grabar(cliente);
        }

        public bool Eliminar(int idCliente)
        {
            return productoDA.Eliminar(idCliente);
        }
    }
}
