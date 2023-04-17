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


        #region guardarSuspiro
        public bool guardarSuspiro(int cliente1, int cliente2)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    string query = "IF NOT EXISTS (SELECT * FROM suspiro1 WHERE cliente1 = @cliente1 and cliente2= @cliente2 and idEstado = 1) BEGIN INSERT INTO suspiro1 (cliente1, cliente2, idEstado, fechaRegistro) VALUES (@cliente1,@cliente2,@idEstado,@fechaRegistro) END";
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);
                    cmd.Parameters.AddWithValue("@cliente2", cliente2);
                    cmd.Parameters.AddWithValue("@idEstado", 1);
                    cmd.Parameters.AddWithValue("@fechaRegistro", DateTime.Now);
                    // Ejecutar la consulta para agregar el nuevo cliente
                    int rows = cmd.ExecuteNonQuery();
                    oconexion.Close();

                    if (rows > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }

                }
            }
            catch (Exception)
            {
                resultado = false;

            }
            return resultado;

        }

        #endregion


        #region Reconsiderado
        public bool Reconsiderado(int cliente1, int cliente2)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    string query = "update suspiro1 set idEstado = 4 where cliente2 = @cliente2 and cliente1 = @cliente1";
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);
                    cmd.Parameters.AddWithValue("@cliente2", cliente2);
                    // Ejecutar la consulta para agregar el nuevo cliente
                    cmd.ExecuteNonQuery();
                    int rows = cmd.ExecuteNonQuery();
                    oconexion.Close();

                    if (rows > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                }
            }
            catch (Exception)
            {
                resultado = false;

            }
            return resultado;

        }

        #endregion


        #region Rechazado
        public bool Rechazado(int cliente1, int cliente2)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    string query = "update suspiro1 set idEstado = 5 where cliente2 = @cliente1 and cliente1 = @cliente2";
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);
                    cmd.Parameters.AddWithValue("@cliente2", cliente2);
                    // Ejecutar la consulta para agregar el nuevo cliente
                    cmd.ExecuteNonQuery();
                    int rows = cmd.ExecuteNonQuery();
                    oconexion.Close();

                    if (rows > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                }
            }
            catch (Exception)
            {
                resultado = false;

            }
            return resultado;

        }
        #endregion

        #region Aceptado
        public bool Aceptado(int cliente1, int cliente2)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    string query = "update suspiro1 set idEstado = 6 where cliente1 = @cliente2 and cliente2 = @cliente1";
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);
                    cmd.Parameters.AddWithValue("@cliente2", cliente2);
                    // Ejecutar la consulta para agregar el nuevo cliente
                    cmd.ExecuteNonQuery();
                    int rows = cmd.ExecuteNonQuery();
                    oconexion.Close();

                    if (rows > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                }
            }
            catch (Exception)
            {
                resultado = false;

            }
            return resultado;
        }

        #endregion




        #region Listar suspiro
        public List<suspiro1> ListarSuspiro(int cliente1)
        {
            List<suspiro1> lista = new List<suspiro1>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "SELECT s.idSuspiro, s.cliente2, c.nombre,c.descripcion, CONVERT(varchar, s.fechaRegistro, 103) AS Fecha FROM suspiro1 s JOIN cliente c ON s.cliente2 = c.idCliente where cliente1= @cliente1 and idEstado = 1";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new suspiro1()
                            {
                                idSuspiro = Convert.ToInt32(dr["idSuspiro"]),
                                cliente2 = Convert.ToInt32(dr["cliente2"]),
                                nombre = dr["nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                Fecha = dr["Fecha"].ToString()


                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<suspiro1>();
            }
            return lista;


        }

        #endregion



        #region listarsuspirosAceptados
        public List<suspiro1> ListarSuspiroAceptados(int cliente1)
        {
            List<suspiro1> lista = new List<suspiro1>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "SELECT s.idSuspiro, s.cliente1, c.nombre, c.descripcion, CONVERT(varchar, s.fechaRegistro, 103) AS Fecha FROM suspiro1 s JOIN cliente c ON s.cliente2 = @cliente1 where cliente1 = c.idCliente and idEstado = 6\r\n";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new suspiro1()
                            {
                                idSuspiro = Convert.ToInt32(dr["idSuspiro"]),
                                cliente2 = Convert.ToInt32(dr["cliente1"]),
                                nombre = dr["nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                Fecha = dr["Fecha"].ToString()



                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<suspiro1>();
            }
            return lista;


        }
        #endregion


        #region ListarSupiroRecibido

        public List<suspiro1> ListarSuspiroRecibido(int cliente2)
        {
            List<suspiro1> lista = new List<suspiro1>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "SELECT s.idSuspiro, s.cliente1, c.nombre,c.descripcion, CONVERT(varchar, s.fechaRegistro, 103) AS Fecha FROM suspiro1 s JOIN cliente c ON s.cliente2 = @cliente2 where cliente1 = c.idCliente and idEstado = 1";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@cliente2", cliente2);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new suspiro1()
                            {
                                idSuspiro = Convert.ToInt32(dr["idSuspiro"]),
                                cliente2 = Convert.ToInt32(dr["cliente1"]),
                                nombre = dr["nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                Fecha = dr["Fecha"].ToString()




                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<suspiro1>();
            }
            return lista;


        }

        #endregion






        #region Me gusta
        public bool matches(int cliente1, int cliente2, int activo)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    string query = "Insert into match1 (cliente1, cliente2, idEstado, fechaRegistro) values (@cliente1, @cliente2, @idEstado, @fechaRegistro)";
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);
                    cmd.Parameters.AddWithValue("@cliente2", cliente2);
                    cmd.Parameters.AddWithValue("@idEstado", activo);
                    cmd.Parameters.AddWithValue("@fechaRegistro", DateTime.Now);
                    // Ejecutar la consulta para agregar el nuevo cliente
                    cmd.ExecuteNonQuery();
                    oconexion.Close();
                    resultado = true;
                }
            }
            catch (Exception)
            {
                resultado = false;

            }
            return resultado;

        }

        #endregion




        #region GuardarMatch
        public bool guardarMatch(int cliente1, int cliente2)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    string query = "insert into matches (cliente1, cliente2, fecha) values (@cliente1,@cliente2,@fechaRegistro)";
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd = new SqlCommand(query, oconexion);
                    // Agregar los parámetros a la consulta
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);
                    cmd.Parameters.AddWithValue("@cliente2", cliente2);
                    cmd.Parameters.AddWithValue("@fechaRegistro", DateTime.Now);
                    // Ejecutar la consulta para agregar el nuevo cliente
                    cmd.ExecuteNonQuery();
                    oconexion.Close();
                    resultado = true;
                }
            }
            catch (Exception)
            {
                resultado = false;

            }
            return resultado;

        }

        #endregion



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
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        cmd.Parameters.AddWithValue("@latitud", latitud);
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


        public descubrir Descubrir(int cliente1, int idCliente)
        {
            descubrir obj = new descubrir();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = " select top 1 c.idCliente, c.nombre, DATEDIFF(year,c.fecha_nac,GETDATE()) as edad, c.descripcion from cliente c  WHERE c.idCliente NOT IN (SELECT cliente2 FROM match1 where cliente1 = @cliente1) and c.idCliente !=  @idCliente  ORDER BY NEWID() ";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);
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


        #region match

        public match1 mostrarMatch(int cliente1, int cliente2, int cliente3)
        {
            match1 obj = new match1();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "select top 1 a.idCliente, a.nombre from cliente a where a.idcliente  in (select cliente2 from match1 b where b.cliente1= @cliente1 and b.idEstado=1) and a.idcliente in (select cliente1 from match1 b where b.cliente2= @cliente2  and b.idEstado=1) and a.idCliente not in (select cliente2 from matches where cliente1 = @cliente3)  ORDER BY NEWID()";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);
                    cmd.Parameters.AddWithValue("@cliente2", cliente2);
                    cmd.Parameters.AddWithValue("@cliente3", cliente3);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new match1()
                            {
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                nombre = dr["nombre"].ToString(),

                            };
                        }
                    }


                }
            }
            catch (Exception)
            {
                obj = new match1();
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