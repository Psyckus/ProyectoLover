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
    public class CD_Preferencias
    {
        public List<preferencia> Listar()
        {
            List<preferencia> lista = new List<preferencia>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    string query = "select p.idPreferencia, p.nombre, p.estado, c.idCategoria_interes, c.nombre[tipo] from preferencia p join categoria_interes c on p.idCategoria_interes = c.idCategoria_interes";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new preferencia()
                            {
                                idPreferencia = Convert.ToInt32(dr["idPreferencia"]),
                                nombre = Convert.ToString(dr["nombre"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                oCategoria_interes = new categoria_interes { idCategoria_interes = Convert.ToInt32(dr["idCategoria_interes"]), nombre = dr["tipo"].ToString() }


                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<preferencia>();
            }
            return lista;

        }
        public List<categoria_interes> ListarCaInt()
        {
            List<categoria_interes> lista = new List<categoria_interes>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "select idCategoria_interes, nombre from categoria_interes";

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
        public int Registrar(preferencia obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarPreferencias", oconexion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.AddWithValue("idCategoria_interes", obj.oCategoria_interes.idCategoria_interes); 
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

        public bool editar(preferencia obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarPreferencia", oconexion);
                    cmd.Parameters.AddWithValue("idPreferencia", obj.idPreferencia);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.AddWithValue("idCategoria_interes", obj.oCategoria_interes.idCategoria_interes);
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
