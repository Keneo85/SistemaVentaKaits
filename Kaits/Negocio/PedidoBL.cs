using System;
using System.Collections.Generic;
using System.Transactions;
using Datos;
using Entidad;

namespace Negocio
{
    public class PedidoBL
    {
        private readonly PedidoDA pedidoDA = new PedidoDA();

        public List<PedidoEN> Listar()
        {
            return pedidoDA.Listar();
        }

        public PedidoEN Obtener(int idPedido)
        {
            return pedidoDA.Obtener(idPedido);
        }

        public int Obtener_Numero_Pedido()
        {
            return pedidoDA.Obtener_Numero_Pedido();
        }

        public bool Grabar(PedidoEN pedido, List<PedidoDetalleEN> listaDetalle)
        {
            bool resultado = false;
            using (TransactionScope transa = new TransactionScope())
            {
                int idPedido = pedidoDA.GrabarPedido(pedido);
                if (idPedido > 0)
                    if (pedidoDA.GrabarPedidoDetalle(listaDetalle, idPedido))
                    {
                        transa.Complete();
                        resultado = true;
                    }
            }
            return resultado;
        }

        public bool Eliminar(int idPedido)
        {
            bool resultado = false;
            using (TransactionScope transa = new TransactionScope())
            {
                var resultadoEliminarDetalle = pedidoDA.EliminarPedidoDetalle(idPedido);
                if (resultadoEliminarDetalle)
                    if (pedidoDA.EliminarPedido(idPedido))
                    {
                        transa.Complete();
                        resultado = true;
                    }
            }
            return resultado;
        }
    }
}
