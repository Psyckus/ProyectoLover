using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace capaPresentacionValidadorPerfiles.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }
        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {

            usuario oUsuario = new usuario();
            oUsuario = new CN_Usuarios().Listar().Where(u => u.email == correo && u.clave == CN_Recurso.ConvertirSha256(clave)).FirstOrDefault();
            Session["Usuario"] = oUsuario;
            if (oUsuario == null)
            {
                //variable temporal
                ViewBag.Error = "Correo o Contraseña no son correctas";
                return View();
            }
            else
            {
                if (oUsuario.Reestablecer)
                {
                    TempData["idUsuario"] = oUsuario.idUsuario;
                    return RedirectToAction("CambiarClave");
                }
                //aqui se valia que rol traer el usuario y lo redirecciona segun cada pagina a que le pertenezca al usuario
                else if (oUsuario.orol.nombre.Contains("Admin"))
                {

                    ViewBag.Error = "Error! Solo validador de perfiles se permiten";
                    return View();
                }
                else if (oUsuario.orol.nombre.Contains("Validador de perfiles"))
                {
                    return RedirectToAction("Index", "Home");


                }
                else if (oUsuario.orol.nombre.Contains("Gestor de indicadores"))
                {

                    ViewBag.Error = "Error! Solo validador de perfiles se permiten";
                    return View();
                }


                return View();
                ViewBag.Error = null;

            }
            //hay que agregar lo demas para poder aplicar la autenticacion en los forms
            //FormsAuthentication.SetAuthCookie(oUsuario.email, false);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarClave(string idusuario, string claveactual, string nuevaclave, string confirmarclave)
        {
            usuario oUsuario = new usuario();
            oUsuario = new CN_Usuarios().Listar().Where(u => u.idUsuario == int.Parse(idusuario)).FirstOrDefault();
            if (oUsuario.clave != CN_Recurso.ConvertirSha256(claveactual))
            {
                TempData["idUsuario"] = idusuario;
                ViewData["vclave"] = "";
                ViewBag.Error = " Contraseña actual no es correcta";
                return View();
            }
            else if (nuevaclave != confirmarclave)
            {
                TempData["idUsuario"] = idusuario;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = " Las Contraseña no coinciden";
                return View();
            }
            else if (nuevaclave == "" || confirmarclave == "")
            {
                TempData["idUsuario"] = idusuario;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = " Debe rellenar los campos vacios";
                return View();
            }
            ViewData["vclave"] = "";
            nuevaclave = CN_Recurso.ConvertirSha256(nuevaclave);
            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idusuario), nuevaclave, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["idUsuario"] = idusuario;
                ViewBag.Error = mensaje;
                return View();
            }

        }


        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            usuario oUsuario = new usuario();
            //recibir el correo que esamos ingresando
            oUsuario = new CN_Usuarios().Listar().Where(item => item.email == correo).FirstOrDefault();
            //validar si hemos encontrado un usuarios
            if (oUsuario == null)
            {

                ViewBag.Error = "No se encontro un usuario relacionado a ese correo";
                return View();
            }
            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().ReestablecerClave(oUsuario.idUsuario, correo, out mensaje);
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
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}