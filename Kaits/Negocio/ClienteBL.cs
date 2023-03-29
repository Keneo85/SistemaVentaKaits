using System;
using System.Collections.Generic;
using Datos;
using Entidad;

namespace Negocio
{

    public class ClienteBL
    {
        private readonly ClienteDA clienteDA = new ClienteDA();
        public List<ClienteEN> Listar()
        {
            return clienteDA.Listar();
        }

        public ClienteEN Obtener(int idCliente)
        {
            return clienteDA.Obtener(idCliente);
        }

        public ClienteEN Buscar(string cadena)
        {
            return clienteDA.Buscar(cadena);
        }

        public bool Actualizar(ClienteEN cliente)
        {
            return clienteDA.Actualizar(cliente);
        }

        public bool Grabar(ClienteEN cliente)
        {
            return clienteDA.Grabar(cliente);
        }

        public bool Eliminar(int idCliente)
        {
            return clienteDA.Eliminar(idCliente);
        }
    }
}
