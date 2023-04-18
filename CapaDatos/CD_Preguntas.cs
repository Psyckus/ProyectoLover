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
    public class CD_Preguntas
    {
        public List<preguntaTest> Listar()
        {
            List<preguntaTest> lista = new List<preguntaTest>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    string query = "select p.idpreguntaTest, p.nombre, t.idtest, t.nombre[tipo] from preguntaTest p join test t on p.idTest = t.idtest";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new preguntaTest()
                            {
                                idPreguntaTest = Convert.ToInt32(dr[" idPreguntaTest"]),
                                nombre = Convert.ToString(dr["nombre"]),
                       
                               otest = new test { idtest = Convert.ToInt32(dr["idtest"]), nombre = dr["tipo"].ToString() }


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
        public List<test> Listart()
        {
            List<test> lista = new List<test>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "select idtest, nombre from test";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new test()
                            {
                                idtest = Convert.ToInt32(dr["idtest"]),
                                nombre = dr["nombre"].ToString(),

                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<test>();
            }
            return lista;

        }
        public int Registrar(preguntaTest obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarPregunta", oconexion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    
                    cmd.Parameters.AddWithValue("idtest", obj.otest.idtest);
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

        public bool editar(preguntaTest obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarPregunta", oconexion);
                    cmd.Parameters.AddWithValue("idPreguntaTest", obj.idPreguntaTest);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    
                    cmd.Parameters.AddWithValue("idtest", obj.otest.idtest);
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
