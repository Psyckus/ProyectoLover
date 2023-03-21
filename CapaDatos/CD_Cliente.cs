using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CapaDatos
{
    public class CD_Cliente
    {

        #region Geolocalizacion


        public bool geolocalizacion(int idC, string latitud, string longitud, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            // Definir la consulta SQL
            string consulta = "select * from ubicacion where idCliente = @idCliente";

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand(consulta, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd.Parameters.AddWithValue("@idCliente", idC);
                    //cmd.Parameters.AddWithValue("@latitud", latitud);
                    //cmd.Parameters.AddWithValue("@logitud", longitud);
                    oconexion.Open();

                    // Ejecutar la consulta y obtener los resultados
                    SqlDataReader result = cmd.ExecuteReader();

                    // Verificar si el cliente existe en la base de datos
                    if (result.HasRows)
                    {

                        // Si el cliente existe, editar los datos
                        result.Read();
                        string idCliente = result["idCliente"].ToString();
                        result.Close();
                        // Definir la consulta SQL para actualizar los datos del cliente

                        consulta = "UPDATE ubicacion SET latitud = @latitud, logitud = @logitud WHERE idCliente = @idCliente";
                        cmd = new SqlCommand(consulta, oconexion);
                        // Agregar los parámetros a la consulta
                        cmd.Parameters.AddWithValue("@idCliente",idCliente);
                        cmd.Parameters.AddWithValue("@latitud",latitud);
                        cmd.Parameters.AddWithValue("@logitud", longitud);

                        // Ejecutar la consulta para actualizar los datos del cliente
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        // Si el cliente no existe, agregarlo a la base de datos
                        result.Close();

                        // Definir la consulta SQL para agregar un nuevo cliente
                        consulta = "INSERT INTO ubicacion (idCliente, latitud, logitud) VALUES (@idCliente, @latitud, @logitud)";
                        cmd = new SqlCommand(consulta, oconexion);

                        // Agregar los parámetros a la consulta
                        cmd.Parameters.AddWithValue("@idCliente", idC);
                        cmd.Parameters.AddWithValue("@latitud", latitud);
                        cmd.Parameters.AddWithValue("@logitud", longitud);
                        // Ejecutar la consulta para agregar el nuevo cliente
                        cmd.ExecuteNonQuery();
                    }

                    // Cerrar la conexión
                    oconexion.Close();
                    resultado = true;

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }



        #endregion

        public int Registrar(cliente obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("email", obj.email);
                    cmd.Parameters.AddWithValue("clave", obj.clave);
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



        #region Descubirir


        public descubrir Descubrir(int idCliente)
        {
            descubrir obj = new descubrir();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "  select top 1 idCliente, nombre, DATEDIFF(year,fecha_nac,GETDATE()) as edad, descripcion from cliente where idCliente != @idCliente order by newid() ";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new descubrir()
                            {
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                nombre = dr["nombre"].ToString(),
                                edad = Convert.ToDouble(dr["edad"]),
                                descripcion = dr["descripcion"].ToString(),

                            };
                        }
                    }


                }
            }
            catch (Exception)
            {
                obj = new descubrir();
            }
            return obj;


        }


        #endregion

        public List<cliente> Listar()
        {
            List<cliente> lista = new List<cliente>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "select idCliente,nombre,email,descripcion,clave,Reestablecer,activo,itsActive  from cliente";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new cliente()
                            {

                                nombre = dr["nombre"].ToString(),
                                email = dr["email"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                clave = dr["clave"].ToString(),
                                Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                activo = Convert.ToBoolean(dr["activo"]),
                                itsActive = Convert.ToBoolean(dr["itsActive"]),
                                idCliente = Convert.ToInt32(dr["idCliente"])


                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<cliente>();
            }
            return lista;


        }

        //cambiar la clave y recuperar clave
        public bool CambiarClave(int idCliente, string nuevaclave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set clave = @nuevaclave, Reestablecer = 0 where idCliente = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;


                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }
        public bool Pregunta1(int idCliente, string fecha_nac, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set fecha_nac = @fecha_nac where idCliente = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@fecha_nac", fecha_nac);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;


                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }
        public bool Pregunta2(int idCliente, string descripcion, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set descripcion = @descripcion, itsActive = 0 where idCliente = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;


                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }
        public bool ReestablecerClave(int idCliente, string clave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update cliente set clave = @clave, Reestablecer = 1 where idCliente = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }
        public bool Pregunta3(int idCliente, HttpPostedFileBase file, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {

                if (file != null && file.ContentLength > 0)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(file.ContentLength);
                    }


                    using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                    {


                        SqlCommand command = new SqlCommand("sp_InsertImage", oconexion);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@rutaFoto", SqlDbType.VarBinary).Value = imageData;
                        command.Parameters.Add("@idCliente", SqlDbType.Int).Value = idCliente;
                        oconexion.Open();
                        resultado = command.ExecuteNonQuery() > 0 ? true : false;
                    }
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
