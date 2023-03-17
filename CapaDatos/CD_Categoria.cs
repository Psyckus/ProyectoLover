using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
namespace CapaDatos
{
    public class CD_Categoria
    {
        public List<categoria_interes> Listar()
        {
            List<categoria_interes> lista = new List<categoria_interes>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    string query = "select * from categoria_interes where idCategoria_interes > 0";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new categoria_interes()
                            {
                                idCategoria_interes = Convert.ToInt32(dr["idCategoria_interes"]),
                                nombre = dr["nombre"].ToString(),
                               
                                estado= Convert.ToBoolean(dr["estado"])


                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<categoria_interes>();
            }
            return lista;

        }
        public int Registrar(categoria_interes obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_registroCategoria", oconexion);
                    
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                   
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idautogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                mensaje = ex.Message;
            }

            return idautogenerado;
        }

        public bool editar(categoria_interes obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_editarCategoria", oconexion);
                    cmd.Parameters.AddWithValue("idCategoria_interes", obj.idCategoria_interes);
                    
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }

    }
}
