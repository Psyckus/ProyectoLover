using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpPost]
        public ActionResult Registrar(cliente objeto)
        {
            int resultado;
            string mensaje = string.Empty;
            ViewData["nombre"] = string.IsNullOrEmpty(objeto.nombre) ? "" : objeto.nombre;
            ViewData["email"] = string.IsNullOrEmpty(objeto.nombre) ? "" : objeto.email;
            if (objeto.clave != objeto.ConfirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                //retornar a la misma vista que se encuentra
                return View();
            }

            resultado = new CN_Clientes().Registrar(objeto, out mensaje);
            if (resultado > 0)
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
                    //Session["Cliente"] = oCliente;
                    return RedirectToAction("Pregunta1", "preguntasInteres");

                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oCliente.email, false);
                    //me permite guardar la informacion del cliente y me deja llamarlo a cualquier controlador 
                    Session["Cliente"] = oCliente;
                    ViewBag.Error = null;
                    //redireccionar al incio de nuestra aplicacion
                    return RedirectToAction("Index", "Index");
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
    }
}