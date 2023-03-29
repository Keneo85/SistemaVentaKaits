using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Entidad;

namespace Datos
{
    public class ClienteDA
    {
        public List<ClienteEN> Listar()
        {
            List<ClienteEN> listaCliente = null;
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Listar_Cliente", con);
                    listaCliente = new List<ClienteEN>();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ClienteEN cliente = new ClienteEN
                                {
                                    Id_Cliente = Convert.ToInt32(dr["Id_Cliente"]),
                                    Nombre = dr["Nombre"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Nombre"]),
                                    ApellidoPaterno = dr["ApellidoPaterno"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ApellidoPaterno"]),
                                    ApellidoMaterno = dr["ApellidoMaterno"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ApellidoMaterno"]),
                                    DNI = dr["DNI"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DNI"])
                                };
                                listaCliente.Add(cliente);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var d = ex.Message;
                throw;
            }
            return listaCliente;
        }

        public ClienteEN Obtener(int idCliente)
        {
            ClienteEN cliente = null;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Obtener_Cliente", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("Id_Cliente", idCliente);

                    using (var dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            cliente = new ClienteEN
                            {
                                Id_Cliente = Convert.ToInt32(dr["Id_Cliente"]),
                                Nombre = dr["Nombre"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Nombre"]),
                                ApellidoPaterno = dr["ApellidoPaterno"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ApellidoPaterno"]),
                                ApellidoMaterno = dr["ApellidoMaterno"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ApellidoMaterno"]),
                                DNI = dr["DNI"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DNI"])
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cliente;
        }

        public ClienteEN Buscar(string cadena)
        {
            ClienteEN cliente = null;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    var cmd = new SqlCommand("Obtener_Cliente", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("Cadena", cadena);

                    using (var dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            cliente = new ClienteEN
                            {
                                Id_Cliente = Convert.ToInt32(dr["Id_Cliente"]),
                                Nombre = dr["Nombre"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Nombre"]),
                                ApellidoPaterno = dr["ApellidoPaterno"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ApellidoPaterno"]),
                                ApellidoMaterno = dr["ApellidoMaterno"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ApellidoMaterno"]),
                                DNI = dr["DNI"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DNI"])
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cliente;
        }

        public bool Actualizar(ClienteEN cliente)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Actualizar_Cliente", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("Id_Cliente", cliente.Id_Cliente);
                    cmd.Parameters.AddWithValue("Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("ApellidoPaterno", cliente.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("ApellidoMaterno", cliente.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("DNI", cliente.DNI);

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

        public bool Grabar(ClienteEN cliente)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Grabar_Cliente", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("ApellidoPaterno", cliente.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("ApellidoMaterno", cliente.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("DNI", cliente.DNI);

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

        public bool Eliminar(int idCliente)
        {
            bool respuesta = false;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Eliminar_Cliente", con)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("Id_Cliente", idCliente);

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
