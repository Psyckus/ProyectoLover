using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocios;

namespace capaPresentacionValidadorPerfiles.Controllers
{
    public class TestPersonalidadController : Controller
    {
        // GET: TestPersonalidad
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Preguntas()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarPregunta()
        {
            List<preguntaTest> oLista = new List<preguntaTest>();
            oLista = new CN_Pregunta().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public JsonResult Listart()
        {

            List<test> oLista = new List<test>();
            oLista = new CN_Pregunta().Listart();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarPregunta(preguntaTest obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.idPreguntaTest == 0)
            {
                resultado = new CN_Pregunta().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Pregunta().editar(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }
        public ActionResult Respuestas()
        {
            return View();
        }



        [HttpGet]
        public JsonResult ListarRes()
        {

            List<respuestaTest> oLista = new List<respuestaTest>();
            oLista = new CN_Respuesta().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarP()
        {

            List<preguntaTest> oLista = new List<preguntaTest>();
            oLista = new CN_Respuesta().ListarP();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarRes(respuestaTest obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.id_respuestaTest == 0)
            {
                resultado = new CN_Respuesta().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Respuesta().editar(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public JsonResult ListarTest()
        {

            List<test> oLista = new List<test>();
            oLista = new CN_Test().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

       

        


        [HttpPost]
        public JsonResult GuardarTest(test obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.idtest == 0)
            {
                resultado = new CN_Test().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Test().editar(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

    }
}