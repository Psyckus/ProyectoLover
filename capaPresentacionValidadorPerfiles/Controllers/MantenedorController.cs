using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocios;
namespace capaPresentacionValidadorPerfiles.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult categoria()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarCategoria()
        {

            List<categoria_interes> oLista = new List<categoria_interes>();
            oLista = new CN_Categoria().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(categoria_interes obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.idCategoria_interes == 0)
            {
                resultado = new CN_Categoria().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().editar(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }
        public ActionResult interes()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarInteres()
        {

            List<interes> oLista = new List<interes>();
            oLista = new CN_Interes().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarCaI()
        {

            List<categoria_interes> oLista = new List<categoria_interes>();
            oLista = new CN_Interes().ListarCaI();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarInteres(interes obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.idinteres == 0)
            {
                resultado = new CN_Interes().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Interes().editar(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }


        public ActionResult preferencia()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarPreferencia()
        {
            List<preferencia> oLista = new List<preferencia>();
            oLista = new CN_Preferencia().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

       
        

        [HttpGet]
        public JsonResult ListarCaInt()
        {

            List<categoria_interes> oLista = new List<categoria_interes>();
            oLista = new CN_Preferencia().ListarCaInt();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarPreferencia(preferencia obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.idPreferencia == 0)
            {
                resultado = new CN_Preferencia().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Preferencia().editar(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }
    }
}