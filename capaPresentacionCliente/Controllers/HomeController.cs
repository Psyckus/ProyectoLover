using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using System.Data.SqlClient;
using CapaDatos;

namespace capaPresentacionCliente.Controllers
{
    public class HomeController : Controller
    {

        private bool clientesObtenidos = false;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Panel()
        {
            return View();
        }

        public ActionResult Principal()
        {
            return View();
        }
        public ActionResult Cuenta()
        {
            return View();
        }

        public ActionResult Geolocalizacion()
        {
            return View();
        }


        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult VerPerfil()
        {
            return View();
        }

        public ActionResult Match()
        {
            return View();
        }


        public ActionResult suspiro()
        {
            return View();
        }


        public ActionResult suspiroRecibido()
        {
            return View();
        }

        public ActionResult suspiroAceptado()
        {
            return View();
        }





        [HttpPost]
        public JsonResult guadarSuspiro(int cliente1, int cliente2)
        {
            bool respuesta = false;


            respuesta = guardarSuspiro(cliente1, cliente2);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);

        }
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
                        // Obtener los correos electrónicos de los clientes
                        string correo1 = ObtenerCorreoCliente(cliente1);
                        string correo2 = ObtenerCorreoCliente(cliente2);
                        string[] nombresClientes = ObtenerNombresClientes(cliente1, cliente2);

                        // Si se obtienen ambos correos electrónicos y nombres de los clientes, enviar notificación de suspiro a ambos clientes
                        if (!string.IsNullOrEmpty(correo1) && !string.IsNullOrEmpty(correo2) && nombresClientes.Length == 2)
                        {
                            string mensaje = string.Format("¡ haz suspirado por alguien por {0}! ", nombresClientes[1]);

                            string asunto1 = "¡Has dado un suspiro!";
                            CN_Recurso.EnviarCorreo(correo1, asunto1, mensaje);

                            mensaje = string.Format("¡{0} han suspirado por ti! Revisa tu cuenta de Lover para ver quién es tu admirador.", nombresClientes[0]);
                            string asunto = "¡Has recibido un suspiro!";
                            CN_Recurso.EnviarCorreo(correo2, asunto, mensaje);
                        }
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
        public void EnviarNotificacionSuspiro(int correoCliente1, int correoCliente2)
        {
            string[] nombresClientes = ObtenerNombresClientes(correoCliente1, correoCliente2);

            if (nombresClientes == null)
            {
                return;
            }

            string mensaje = $"¡{nombresClientes[1]}, {nombresClientes[0]} te ha enviado un suspiro! Revisa tu cuenta de Lover2023Cuc para ver quién es tu admirador.";
            string asunto = $"¡{nombresClientes[1]}, has recibido un suspiro de {nombresClientes[0]}!";

            string correo1 = ObtenerCorreoCliente(correoCliente1);
            string correo2 = ObtenerCorreoCliente(correoCliente2);

            if (!string.IsNullOrEmpty(correo1))
            {
                CN_Recurso.EnviarCorreo(correo1, asunto, mensaje);
            }

            if (!string.IsNullOrEmpty(correo2))
            {
                CN_Recurso.EnviarCorreo(correo2, asunto, mensaje);
            }
        }
        [HttpPost]
        public JsonResult Reconsiderado(int cliente1, int cliente2)
        {
            bool respuesta = false;


            respuesta = new CN_Clientes().Reconsiderado(cliente1, cliente2);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Rechazado(int cliente1, int cliente2)
        {
            bool respuesta = false;


            respuesta = new CN_Clientes().Rechazado(cliente1, cliente2);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Aceptado(int cliente1, int cliente2)
        {
            bool respuesta = false;


            respuesta = new CN_Clientes().Aceptado(cliente1, cliente2);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public JsonResult ListarSuspiro(int cliente1)
        {

            List<suspiro1> oLista = new List<suspiro1>();
            oLista = new CN_Clientes().ListarSuspiro(cliente1);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ListarSuspiroAceptados(int cliente1)
        {

            List<suspiro1> oLista = new List<suspiro1>();
            oLista = new CN_Clientes().ListarSuspiroAceptados(cliente1);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult ListarSuspiroRecibido(int cliente2)
        {

            List<suspiro1> oLista = new List<suspiro1>();
            oLista = new CN_Clientes().ListarSuspiroRecibido(cliente2);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }







        [HttpPost]
        public JsonResult Matches(int cliente1, int cliente2, int activo)
        {
            bool respuesta = false;
           

            respuesta = new CN_Clientes().match(cliente1, cliente2, activo);

            return Json(new { resultado = respuesta}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult guadarMatches(int cliente1, int cliente2)
        {
            bool respuesta = false;


            respuesta = new CN_Clientes().guardarMatch(cliente1, cliente2);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult VerP(int cliente1, int idCliente)
        {

            descubrir obj = new CN_Clientes().Descubrir(cliente1, idCliente);
            return Json(new { data = obj }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Descubrir(int cliente1, int idCliente)
        {

            descubrir obj = new CN_Clientes().Descubrir(cliente1,idCliente);
            return Json(new { data = obj }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult mostrarMatch(int cliente1, int cliente2, int cliente3)
        {

            match1 obj = new CN_Clientes().mostrarMatch(cliente1, cliente2, cliente3);

            if (obj.idCliente == 0 || obj.nombre == null)
            {
                return Json(new { data = obj }, JsonRequestBehavior.AllowGet);
            }

            if (!clientesObtenidos)
            {
                ObtenerClientes(cliente1);
                clientesObtenidos = true;
                ActualizarEstadoMatch(cliente1);

            }
            return Json(new { data = obj }, JsonRequestBehavior.AllowGet);

        }

        public void ActualizarEstadoMatch(int cliente1)
        {
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    oconexion.Open();

                    string query = "UPDATE match1 SET idEstado = @idEstado WHERE cliente1 = @cliente1 ";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idEstado", 7); // 2 es el ID del nuevo estado
                    cmd.Parameters.AddWithValue("@cliente1", cliente1);


                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Manejar excepción
            }
        }

        public void ObtenerClientes(int cliente1)
        {
            match1 obj = new match1();
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                string query = "select top 1 a.idCliente, a.nombre from cliente a where a.idcliente  in (select cliente2 from match1 b where b.cliente1= @cliente1 and b.idEstado=1) and a.idcliente in (select cliente1 from match1 b where b.cliente2= @cliente2  and b.idEstado=1) and a.idCliente not in (select cliente2 from matches where cliente1 = @cliente3)  ORDER BY NEWID()";

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.Parameters.AddWithValue("@cliente1", cliente1);
                cmd.Parameters.AddWithValue("@cliente2", cliente1);
                cmd.Parameters.AddWithValue("@cliente3", cliente1);
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

                        // Hacer algo con el idCliente2 obtenido
                        EnviarNotificacionMatch(cliente1, obj.idCliente);

                    }
                }
            }
        }
        public bool EnviarNotificacionMatch(int cliente1, int cliente2)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    // Obtener los correos electrónicos de los clientes
                    string correo1 = ObtenerCorreoCliente(cliente1);
                    string correo2 = ObtenerCorreoCliente(cliente2);

                    // Si se obtienen ambos correos electrónicos, enviar notificación de match a ambos clientes
                    if (!string.IsNullOrEmpty(correo1) && !string.IsNullOrEmpty(correo2))
                    {
                        // Obtener los nombres de los clientes
                        string[] nombresClientes = ObtenerNombresClientes(cliente1, cliente2);
                        string mensaje1 = $"¡Felicidades {nombresClientes[0]}! Has hecho match con {nombresClientes[1]}. ¡Empieza una conversación ahora!";
                        string mensaje2 = $"¡Felicidades {nombresClientes[1]}! Has hecho match con {nombresClientes[0]}. ¡Empieza una conversación ahora!";
                        string asunto = "¡Has hecho match!";

                        // Enviar notificación de match a ambos clientes
                        CN_Recurso.EnviarCorreo(correo1, asunto, mensaje1);
                        CN_Recurso.EnviarCorreo(correo2, asunto, mensaje2);
                        // Actualizar el estado del match para que no vuelva a ser notificado


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

        public string[] ObtenerNombresClientes(int idCliente1, int idCliente2)
        {
            string[] nombres = new string[2];
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "SELECT nombre FROM cliente WHERE idCliente = @idCliente";
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente1);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        nombres[0] = reader.GetString(0);
                    }

                    reader.Close();

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idCliente", idCliente2);
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        nombres[1] = reader.GetString(0);
                    }

                    reader.Close();
                    oconexion.Close();
                }
            }
            catch (Exception)
            {
                nombres = null;
            }

            return nombres;
        }
        public string ObtenerCorreoCliente(int idCliente)
        {
            string correo = null;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    string query = "SELECT email FROM cliente WHERE idCliente = @idCliente";
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        correo = reader.GetString(0);
                    }

                    reader.Close();
                    oconexion.Close();
                }
            }
            catch (Exception)
            {
                correo = null;
            }

            return correo;
        }

        [HttpGet]
        public JsonResult ListarUsuario()
        {

            List<usuario> oLista = new List<usuario>();
            oLista = new CN_Usuarios().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarTipoID()
        {

            List<tipoIdentificacion> oLista = new List<tipoIdentificacion>();
            oLista = new CN_Usuarios().ListarTipoID();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarRoles()
        {

            List<rol> oLista = new List<rol>();
            oLista = new CN_Usuarios().ListarRoles();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarUser(usuario obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.idUsuario == 0)
            {
                resultado = new CN_Usuarios().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().editar(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public JsonResult EliminarUser(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

    }
}
