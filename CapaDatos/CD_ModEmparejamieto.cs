using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ModEmparejamieto
    {

        public List<ModEmparejamiento> ListaModulo()
        {
            List<ModEmparejamiento> lista = new List<ModEmparejamiento>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "select CriterioInteres,EdadCriterio,TestCriterio from ModEmparejamiento";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ModEmparejamiento()
                            {
                                CriterioInteres = Convert.ToInt32(dr["CriterioInteres"].ToString()),
                                EdadCriterio = Convert.ToBoolean(dr["EdadCriterio"]),
                                TestCriterio = Convert.ToBoolean(dr["TestCriterio"])

                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<ModEmparejamiento>();
            }
            return lista;
        }

        public bool editarModulo(ModEmparejamiento obj, out string mensaje)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarModulo", oconexion);
                    cmd.Parameters.AddWithValue("idModConf", obj.idModConf);
                    cmd.Parameters.AddWithValue("interes", obj.CriterioInteres);
                    cmd.Parameters.AddWithValue("edad", obj.EdadCriterio);
                    cmd.Parameters.AddWithValue("test", obj.TestCriterio);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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
