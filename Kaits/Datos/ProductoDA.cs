using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;

namespace Datos
{
    public class ProductoDA
    {
        public List<ProductoEN> Listar()
        {
            List<ProductoEN> listaProducto = null;
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    var cmd = new SqlCommand("Listar_Producto", con);
                    listaProducto = new List<ProductoEN>();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ProductoEN producto = new ProductoEN();
                                producto.Id_Producto = Convert.ToInt32(dr["Id_Producto"]);
                                producto.Descripcion = dr["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Descripcion"]);
                                producto.Precio = dr["Precio"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Precio"]);
                                listaProducto.Add(producto);
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
            return listaProducto;
        }

        public ProductoEN Obtener(int idProducto)
        {
            ProductoEN producto = null;

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    var cmd = new SqlCommand("Obtener_Producto", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id_Producto", idProducto);

                    using (var dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            producto = new ProductoEN
                            {
                                Id_Producto = Convert.ToInt32(dr["Id_Producto"]),
                                Descripcion = dr["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Descripcion"]),
                                Precio = dr["Precio"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Precio"])
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return producto;
        }

        public ProductoEN Buscar(string descripcion)
        {
            ProductoEN producto = null;

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    var cmd = new SqlCommand("Buscar_Producto", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Descripcion", descripcion);

                    using (var dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        if (dr.HasRows)
                        {
                            producto = new ProductoEN
                            {
                                Id_Producto = Convert.ToInt32(dr["Id_Producto"]),
                                Descripcion = dr["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Descripcion"]),
                                Precio = dr["Precio"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Precio"])
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return producto;
        }

        public bool Actualizar(ProductoEN producto)
        {
            bool respuesta = false;

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    var cmd = new SqlCommand("Actualizar_Producto", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id_Producto", producto.Id_Producto);
                    cmd.Parameters.AddWithValue("Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("Precio", producto.Precio);

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

        public bool Grabar(ProductoEN producto)
        {
            bool respuesta = false;

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    var cmd = new SqlCommand("Grabar_Producto", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("Precio", producto.Precio);

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

        public bool Eliminar(int idProducto)
        {
            bool respuesta = false;

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
                {
                    con.Open();
                    var cmd = new SqlCommand("Eliminar_Producto", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id_Producto", idProducto);

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
