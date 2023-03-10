using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace capaPresentacionValidadorPerfiles.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
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