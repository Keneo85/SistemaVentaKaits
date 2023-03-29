using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Entidad;

namespace Datos
{
    public class PedidoDA
    {
        public List<PedidoEN> Listar()
        {
            List<PedidoEN> listaPedido = null;
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Listar_Pedido", con);
                    listaPedido = new List<PedidoEN>();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                PedidoEN en = new PedidoEN
                                {
                                    Id_Pedido = Convert.ToInt32(dr["Id_Pedido"]),
                                    Id_Cliente = Convert.ToInt32(dr["Id_Cliente"]),
                                    Cliente = dr["Cliente"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Cliente"]),
                                    Fecha_Pedido = dr["Fecha_Pedido"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["Fecha_Pedido"]),
                                    Total_Pedido = dr["Total_Pedido"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Total_Pedido"])
                                };
                                listaPedido.Add(en);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return listaPedido;
        }

        public PedidoEN Obtener(int idPedido)
        {
            PedidoEN pedido = null;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Obtener_Pedido", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("Id_Pedido", idPedido);

                    using (var dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            pedido = new PedidoEN
                            {
                                Id_Pedido = Convert.ToInt32(dr["Id_Pedido"]),
                                Id_Cliente = Convert.ToInt32(dr["Id_Cliente"]),
                                Cliente = dr["Cliente"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Cliente"]),
                                Fecha_Pedido = dr["Fecha_Pedido"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["Fecha_Pedido"]),
                                Total_Pedido = dr["Total_Pedido"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Total_Pedido"])
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return pedido;
        }

        public int Obtener_Numero_Pedido()
        {
            var nropedido = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Obtener_Numero_Pedido", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    using (var dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            nropedido = Convert.ToInt32(dr["NroPedido"]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return nropedido;
        }

        public int GrabarPedido(PedidoEN pedido)
        {
            int id = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Grabar_Pedido", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("Id_Pedido", pedido.Id_Pedido);
                    cmd.Parameters.AddWithValue("Fecha_Pedido", pedido.Fecha_Pedido);
                    cmd.Parameters.AddWithValue("Id_Cliente", pedido.Id_Cliente);
                    cmd.Parameters.AddWithValue("Total_Pedido", pedido.Total_Pedido);
                    SqlParameter ret = cmd.Parameters.Add("@NroPedido", SqlDbType.Int);
                    ret.Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(ret.Value);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return id;
        }

        public bool GrabarPedidoDetalle(List<PedidoDetalleEN> listaDetalle, int idPedido)
        {
            bool respuesta = false;

            try
            {
                foreach (var item in listaDetalle)
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Grabar_Detalle_Pedido", con)
                        {
                            CommandType = System.Data.CommandType.StoredProcedure
                        };

                        cmd.Parameters.AddWithValue("Id_Pedido", idPedido);
                        cmd.Parameters.AddWithValue("Id_Producto", item.Id_Producto);
                        cmd.Parameters.AddWithValue("Cantidad", item.Cantidad);
                        cmd.Parameters.AddWithValue("Precio_Unitario", item.Precio_Unitario);
                        cmd.Parameters.AddWithValue("Subtotal", item.Subtotal);
                        cmd.Parameters.AddWithValue("IGV", item.IGV);
                        if (cmd.ExecuteNonQuery() > 0)
                            respuesta = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }

        public bool EliminarPedido(int idPedido)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Eliminar_Pedido", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("Id_Pedido", idPedido);

                    if (Convert.ToInt32(cmd.ExecuteNonQuery()) > 0)
                        respuesta = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }

        public bool EliminarPedidoDetalle(int idPedido)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Eliminar_Detalle_Pedido", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("Id_Pedido", idPedido);

                    if (Convert.ToInt32(cmd.ExecuteNonQuery()) > 0)
                        respuesta = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
    }
}
