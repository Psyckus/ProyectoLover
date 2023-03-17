using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
namespace CapaDatos
{
   public class CD_Interes
    {
        public List<interes> Listar()
        {
            List<interes> lista = new List<interes>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    string query = "select i.idinteres, i.nombre, i.estado, c.idCategoria_interes, c.nombre from interes i join categoria_interes c on i.idCategoria_interes = c.idCategoria_interes";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new interes()
                            {
                                idinteres = Convert.ToInt32(dr["idinteres"]),
                                nombreI = Convert.ToString(dr["nombre"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                oCategoria_interes = new categoria_interes { idCategoria_interes = Convert.ToInt32(dr["idCategoria_interes"]), nombre = dr["nombre"].ToString()}


                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<interes>();
            }
            return lista;

        }
        public List<categoria_interes> ListarCaI()
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
    }
}
