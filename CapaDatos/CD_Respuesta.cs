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
    public class CD_Respuesta
    {
        public List<respuestaTest> Listar()
        {
            List<respuestaTest> lista = new List<respuestaTest>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {



                    string query = "select r.id_respuestaTest, r.respuesta, p.idpreguntaTest, p.nombre[tipo] from respuestaTest r join preguntaTest p on r.idPregunta = p.idpreguntaTest";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new respuestaTest()
                            {
                                id_respuestaTest = Convert.ToInt32(dr["id_respuestaTest"]),
                                respuesta = Convert.ToString(dr["respuesta"]),
                                
                                opregunta = new preguntaTest { idPreguntaTest = Convert.ToInt32(dr["idpreguntaTest"]), nombre = dr["tipo"].ToString() }


                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<respuestaTest>();
            }
            return lista;

        }
        public List<preguntaTest> ListarP()
        {
            List<preguntaTest> lista = new List<preguntaTest>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "select idpreguntaTest, nombre from preguntaTest";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new preguntaTest()
                            {
                                idPreguntaTest = Convert.ToInt32(dr["idpreguntaTest"]),
                                nombre = dr["nombre"].ToString(),

                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<preguntaTest>();
            }
            return lista;

        }
        public int Registrar(respuestaTest obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarRes", oconexion);
                    cmd.Parameters.AddWithValue("respuesta", obj.respuesta);
                    
                    cmd.Parameters.AddWithValue("idPreguntaTest", obj.opregunta.idPreguntaTest);
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

        public bool editar(respuestaTest obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarRes", oconexion);
                    cmd.Parameters.AddWithValue("id_respuestaTest", obj.id_respuestaTest);
                    cmd.Parameters.AddWithValue("respuesta", obj.respuesta);
                   
                    cmd.Parameters.AddWithValue("idPreguntaTest", obj.opregunta.idPreguntaTest);
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
