using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace capaPresentacionCliente.Controllers
{
    public class preguntasInteresController : Controller
    {
        // GET: preguntasInteres
        public ActionResult Pregunta1()
        {
            return View();
        }
        public ActionResult Pregunta2()
        {
            return View();
        }
        public ActionResult Pregunta3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pregunta1(string idCliente, string fecha_nac)
        {
            cliente oCliente = new cliente();
            oCliente = new CN_Clientes().Listar().Where(u => u.idCliente == int.Parse(idCliente)).FirstOrDefault();
            string mensaje = string.Empty;
            bool respuesta = new CN_Clientes().Pregunta1(int.Parse(idCliente), fecha_nac, out mensaje);
            if (respuesta)
            {
                TempData["idCliente"] = idCliente;
                return RedirectToAction("Pregunta2", "preguntasInteres");
            }
            else
            {
                TempData["idCliente"] = idCliente;
                ViewBag.Error = mensaje;
                return View();
            }

        }
        [HttpPost]
        public ActionResult Pregunta2(string idCliente, string descripcion)
        {
            cliente oCliente = new cliente();
            oCliente = new CN_Clientes().Listar().Where(u => u.idCliente == int.Parse(idCliente)).FirstOrDefault();
            string mensaje = string.Empty;
            bool respuesta = new CN_Clientes().Pregunta2(int.Parse(idCliente), descripcion, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index", "Index");
            }
            else
            {
                TempData["idCliente"] = idCliente;
                ViewBag.Error = mensaje;
                return View();
            }

        }
       
    }
}