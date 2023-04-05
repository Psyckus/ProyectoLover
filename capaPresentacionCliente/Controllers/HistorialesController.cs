using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace capaPresentacionCliente.Controllers
{
    public class HistorialesController : Controller
    {
        // GET: Historiales
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VistaMeGusta()

        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;
            var idCliente = oClienteSession.idCliente;
            List<cliente> clientesGustados = new CN_Historial().ObtenerClientesGustados(idCliente);
            ViewBag.ClientesGustados = clientesGustados;
            return View();
        }
        public ActionResult VistaQuienLeGusto()

        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;
            var idCliente = oClienteSession.idCliente;
            List<cliente> clientesQueinMeGusta = new CN_Historial().ObtenerClientesQuienMeGusta(idCliente);
            ViewBag.clientesQueinMeGusta = clientesQueinMeGusta;
            return View();
        }
        public ActionResult VistaYaNoMeGusta()
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;
            var idCliente = oClienteSession.idCliente;
            List<cliente> clientesQuienYaNoMeGusta = new CN_Historial().ObtenerClientesYaNoMeGusta(idCliente);
            ViewBag.ObtenerClientesYaNoMeGusta = clientesQuienYaNoMeGusta;
            return View();

        }
        [HttpGet]
        public ActionResult HistorialMeGusta()
        {

            return RedirectToAction("VistaMeGusta");
        }
        [HttpGet]
        public ActionResult HistorialQuienMeGusta()
        {

            return RedirectToAction("VistaQuienLeGusto");
        }
        [HttpGet]
        public ActionResult HistorialYaNoMeGusta()

        {

            return RedirectToAction("VistaYaNoMeGusta");

        }

        [HttpPost]
        public JsonResult ReconsiderarCliente(int idCliente)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                oconexion.Open();
                SqlCommand cmd = new SqlCommand("update match1 SET idEstado = 3 WHERE cliente2 = @idcliente", oconexion);
                cmd.Parameters.AddWithValue("@idcliente", idCliente);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Json(new { success = true, message = "Se ha reconsiderado tu me gusta." });
                }
                else
                {
                    return Json(new { success = false, message = "No se pudo realizar la operación." });
                }
            }
        }



        //public ActionResult GetImage(int idMatch)
        //{
        //    byte[] imageData = null;

        //    using (SqlConnection oconexion = new SqlConnection(conexion.cn))
        //    {
        //        oconexion.Open();

        //        SqlCommand command = new SqlCommand("SELECT rutaFoto FROM foto WHERE idCliente = (SELECT cliente2 FROM match1 WHERE idMatch = @idMatch)", oconexion);
        //        command.Parameters.Add("@idMatch", SqlDbType.Int).Value = idMatch;

        //        SqlDataReader reader = command.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            imageData = (byte[])reader["rutaFoto"];
        //        }
        //    }

        //    return File(imageData, "image/jpeg");
        //}


    }
}