using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Historial
    {

        public List<cliente> ObtenerClientesGustados(int idCliente)
        {
            List<cliente> clientesGustados = new List<cliente>();

            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                oconexion.Open();

                string sql = "SELECT c.* FROM cliente c " +
                             "INNER JOIN match1 m ON c.idCliente = m.cliente2 " +
                             "WHERE m.cliente1 = @idCliente and  m.idEstado = 1";

                SqlCommand cmd = new SqlCommand(sql, oconexion);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente cliente = new cliente();

                    cliente.idCliente = Convert.ToInt32(reader["idCliente"]);
                    cliente.nombre = reader["nombre"].ToString();
                    cliente.descripcion = reader["descripcion"].ToString();
                    // Obtener otros campos de cliente según sea necesario

                    clientesGustados.Add(cliente);
                }
            }

            return clientesGustados;
        }
        public List<cliente> ObtenerClientesQuienMeGusta(int idCliente)
        {
            List<cliente> clientesQuienMeGusta = new List<cliente>();

            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                oconexion.Open();

                string sql = "SELECT c.*, m.fecha FROM cliente c " +
                             "INNER JOIN matches m ON c.idCliente = m.cliente2 " +
                             "WHERE m.cliente1 = @idCliente";

                SqlCommand cmd = new SqlCommand(sql, oconexion);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
              
                    cliente cliente = new cliente();

                    cliente.idCliente = Convert.ToInt32(reader["idCliente"]);
                    cliente.nombre = reader["nombre"].ToString();
                    //cliente.fecha_Registro = reader["fecha_Registro"].ToString();
        
                    // Obtener otros campos de cliente según sea necesario

                    clientesQuienMeGusta.Add(cliente);
                }
            }

            return clientesQuienMeGusta;
        }

        public List<cliente> ObtenerClientesYaNoMeGusta(int idCliente)
        {
            List<cliente> clientesYaNoMeGusta = new List<cliente>();

            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                oconexion.Open();

                string sql = "SELECT c.* FROM cliente c " +
                             "INNER JOIN match1 m ON c.idCliente = m.cliente2 " +
                             "WHERE m.cliente1 = @idCliente and  m.idEstado = 3";

                SqlCommand cmd = new SqlCommand(sql, oconexion);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente cliente = new cliente();

                    cliente.idCliente = Convert.ToInt32(reader["idCliente"]);
                    cliente.nombre = reader["nombre"].ToString();
             
                    
                    // Obtener otros campos de cliente según sea necesario

                    clientesYaNoMeGusta.Add(cliente);
                }
            }

            return clientesYaNoMeGusta;
        }

    }
}
