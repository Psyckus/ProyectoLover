using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loverSitios.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        public ActionResult Sitio()
        {
            return View();
        }
        public ActionResult Sistema()
        {
            return View();
        }
        public ActionResult Servidor()
        {
            return View();
        }

    }
}