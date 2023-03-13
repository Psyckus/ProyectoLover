using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<usuario> Listar()
        {
            List<usuario> lista = new List<usuario>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select u.idUsuario, u.identificacion, u.nombre, u.apellido,");
                    sb.AppendLine("u.email,  u.clave, u.Reestablecer, r.idRol, r.nombre[Rol],");
                    sb.AppendLine("i.idTipoIdentificacion, i.nombre[Tipo] , u.activo ");
                    sb.AppendLine("from usuario u");
                    sb.AppendLine("inner join rol r on r.idRol = u.idRol");
                    sb.AppendLine("inner join tipoIdentificacion i on i.idTipoIdentificacion = u.tipoIdentificacion");


                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new usuario()
                            {
                                idUsuario = Convert.ToInt32(dr["idUsuario"]),
                                identificacion = Convert.ToInt32(dr["identificacion"]),
                                nombre = dr["nombre"].ToString(),
                                apellido = dr["apellido"].ToString(),
                                email = dr["email"].ToString(),
                                clave = dr["clave"].ToString(),
                                Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                orol = new rol {idRol = Convert.ToInt32(dr["idRol"]), nombre = dr["Rol"].ToString()},
                                otipoIdentificacion = new tipoIdentificacion { idTipoIdentificacion = Convert.ToInt32(dr["idTipoIdentificacion"]), nombre = dr["Tipo"].ToString() },
                                activo = Convert.ToBoolean(dr["activo"])


                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<usuario>();
            }
            return lista;

        }


        public int Registrar(usuario obj, out string mensaje)
        {
            int idautogenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("idRol", obj.orol.idRol);
                    cmd.Parameters.AddWithValue("identificacion", obj.identificacion);
                    cmd.Parameters.AddWithValue("tipoIdentificacion", obj.otipoIdentificacion.idTipoIdentificacion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("email", obj.email);
                    cmd.Parameters.AddWithValue("clave", obj.clave);
                    cmd.Parameters.AddWithValue("activo", obj.activo);
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

        public bool editar(usuario obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("idUsuario", obj.idUsuario);
                    cmd.Parameters.AddWithValue("idRol", obj.orol.idRol);
                    cmd.Parameters.AddWithValue("identificacion", obj.identificacion);
                    cmd.Parameters.AddWithValue("tipoIdentificacion", obj.otipoIdentificacion.idTipoIdentificacion);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("email", obj.email);
                    cmd.Parameters.AddWithValue("activo", obj.activo);
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

        public bool eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top(1) from usuario where idUsuario = @idUusario", oconexion);
                    cmd.Parameters.AddWithValue("@idUusario", id);
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


        public List<tipoIdentificacion> ListarTipoID()
        {
            List<tipoIdentificacion> lista = new List<tipoIdentificacion>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "select idTipoIdentificacion, nombre from tipoIdentificacion";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new tipoIdentificacion()
                            {
                                idTipoIdentificacion = Convert.ToInt32(dr["idTipoIdentificacion"]),
                                 nombre = dr["nombre"].ToString(),
                 
                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<tipoIdentificacion>();
            }
            return lista;

        }

        public List<rol> ListarRoles()
        {
            List<rol> lista = new List<rol>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "select idRol, nombre from rol";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new rol()
                            {
                                idRol = Convert.ToInt32(dr["idRol"]),
                                nombre = dr["nombre"].ToString(),

                            });
                        }
                    }


                }
            }
            catch (Exception)
            {
                lista = new List<rol>();
            }
            return lista;

        }


        public bool CambiarClave(int idusuario, string nuevaclave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update usuario set clave = @nuevaclave, Reestablecer = 0 where idusuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idusuario);
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
        public bool ReestablecerClave(int idusuario, string clave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update usuario set clave = @clave, Reestablecer = 1 where idusuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idusuario);
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


    }
}
