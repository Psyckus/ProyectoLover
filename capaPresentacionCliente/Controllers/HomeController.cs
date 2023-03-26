using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;

namespace capaPresentacionCliente.Controllers
{
    public class HomeController : Controller
    {

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


        [HttpPost]
        public JsonResult Matches(int cliente1, int cliente2, int activo)
        {
            bool respuesta = false;
           

            respuesta = new CN_Clientes().match(cliente1, cliente2, activo);

            return Json(new { resultado = respuesta}, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public JsonResult Descubrir(int idCliente)
        {

            descubrir obj = new CN_Clientes().Descubrir(idCliente);
            return Json(new { data = obj }, JsonRequestBehavior.AllowGet);

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
