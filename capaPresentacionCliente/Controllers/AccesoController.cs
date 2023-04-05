using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace capaPresentacionCliente.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult Reestablecer()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }

        public async Task<ActionResult> DobleAutenticacion()
        {
           await GenerarCodigo();
            return View();
        }
        #region ubicacion

        [HttpPost]
        public JsonResult ubicacion(int idCliente, string latitud, string longitud)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Clientes().geolocalizacion(idCliente, latitud, longitud, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        #endregion




        [HttpPost]
        public ActionResult Registrar(cliente objeto)
        {
            int resultado;
            string mensaje = string.Empty;
            ViewData["nombre"] = string.IsNullOrEmpty(objeto.nombre) ? "" : objeto.nombre;
            ViewData["email"] = string.IsNullOrEmpty(objeto.nombre) ? "" : objeto.email;
            if (objeto.clave != objeto.ConfirmarClave)
            {
                //ViewBag.Error = "Las contraseñas no coinciden";
                //retornar a la misma vista que se encuentra
                var error = new { message = "Las contraseñas no coinciden", success = false };
                return Json(error, JsonRequestBehavior.AllowGet);

            }

            resultado = new CN_Clientes().Registrar(objeto, out mensaje);
            if (resultado > 0)
            {

                var success = new { message = "Usuario registrado exitosamente", success = true };
                return Json(success, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var error = new { message = "Error no se pudo registrar el usuario", success = false };
                return Json(error, JsonRequestBehavior.AllowGet);

            }


        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            cliente oCliente = null;
            oCliente = new CN_Clientes().Listar().Where(item => item.email == correo && item.clave == CN_Recurso.ConvertirSha256(clave)).FirstOrDefault();

            if (oCliente == null)
            {
                ViewBag.Error = "Correo o contraseña no son correctas ";
                return View();
            }
            else
            {
                if (oCliente.Reestablecer)
                {
                    //guardar datos temporalmente
                    TempData["idCliente"] = oCliente.idCliente;
                    return RedirectToAction("CambiarClave", "Acceso");

                }
                else if (oCliente.itsActive)
                {
                    TempData["idCliente"] = oCliente.idCliente;
                    //TempData["itsActive"] = oCliente.itsActive;
                    Session["Cliente"] = oCliente;
                    return RedirectToAction("Pregunta1", "preguntasInteres");

                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oCliente.email, false);
                    //me permite guardar la informacion del cliente y me deja llamarlo a cualquier controlador 
                    Session["Cliente"] = oCliente;
                    ViewBag.Error = null;
                    //redireccionar al incio de nuestra aplicacion
                    return RedirectToAction("DobleAutenticacion");
                }
            }

        }
        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            cliente oCliente = new cliente();
            //recibir el correo que esamos ingresando
            oCliente = new CN_Clientes().Listar().Where(item => item.email == correo).FirstOrDefault();
            //validar si hemos encontrado un usuarios
            if (oCliente == null)
            {

                ViewBag.Error = "No se encontro un cliente relacionado a ese correo";
                return View();
            }
            string mensaje = string.Empty;
            bool respuesta = new CN_Clientes().ReestablecerClave(oCliente.idCliente, correo, out mensaje);
            //validar la respuesta
            if (respuesta)
            {

                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");


            }
            else
            {
                ViewBag.Error = mensaje;
                return View();

            }
        }
        [HttpPost]
        public ActionResult CambiarClave(string idCliente, string claveactual, string nuevaclave, string confirmarclave)
        {
            cliente oCliente = new cliente();
            oCliente = new CN_Clientes().Listar().Where(u => u.idCliente == int.Parse(idCliente)).FirstOrDefault();
            if (oCliente.clave != CN_Recurso.ConvertirSha256(claveactual))
            {
                TempData["idCliente"] = idCliente;
                ViewData["vclave"] = "";
                ViewBag.Error = " Contraseña actual no es correcta";
                return View();
            }
            else if (nuevaclave != confirmarclave)
            {
                TempData["idCliente"] = idCliente;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = " Las Contraseña no coinciden";
                return View();
            }
            ViewData["vclave"] = "";
            nuevaclave = CN_Recurso.ConvertirSha256(nuevaclave);
            string mensaje = string.Empty;
            bool respuesta = new CN_Clientes().CambiarClave(int.Parse(idCliente), nuevaclave, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["idCliente"] = idCliente;
                ViewBag.Error = mensaje;
                return View();
            }
        }
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "index");
        }
      
        public async Task<JsonResult> GenerarCodigo()
        {
            // Buscar al cliente por correo electrónico
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;
            var idCliente = oClienteSession.idCliente;
            var Correo = oClienteSession.email;
            if (oClienteSession == null)
            {
                return Json(new { success = false, message = "El cliente no existe" });
            }

            // Generar un código aleatorio de seis dígitos

            var codigo = CN_Recurso.GenerarCodigo();

            // Crear un registro en la tabla de códigos de autenticación
            var codigoAutenticacion = new CodigoAutenticacion
            {
                IdCliente = idCliente,
                Codigo = codigo
            };
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO codigo_autenticacion (idcliente, codigo) VALUES (@idcliente,@codigo)", oconexion);
                cmd.Parameters.AddWithValue("@idcliente", idCliente);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }

            // Enviar correo electrónico con el código de autenticación
            var asunto = "Código de autenticación";
            var mensaje = $"Su código de autenticación es: {codigo}";
            await CN_Recurso.EnviarCorreoAsync(Correo, asunto, mensaje);

            return Json(new { success = true, message = "Código generado correctamente" });
        }
        public async Task<JsonResult> VerificarCodigo(string codigo)
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;
            var idCliente = oClienteSession.idCliente;

            // Verificar si el código es válido
            var codigoValido = await CN_Recurso.ValidarCodigoAsync(idCliente, codigo);

            if (codigoValido)
            {
                // Redirigir al usuario a la página correspondiente si el código es válido
                return Json(new { success = true, message = "Código verificado correctamente" });
            }
            else
            {
                // Si el código no es válido, mostrar un mensaje de error
                //ViewBag.ErrorMessage = "El código ingresado no es válido";
                return Json(new { success = false, message = "Código incorrecto" });
            }
        }

    }
}
