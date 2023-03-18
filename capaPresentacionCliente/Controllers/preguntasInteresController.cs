using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaDatos;

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

        //metodo para obtener la imagen/solo una imagen tipo jpeg
        public ActionResult GetImage(int id)
        {
            byte[] imageData = null;


            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                oconexion.Open();

                SqlCommand command = new SqlCommand("SELECT rutaFoto FROM foto WHERE idCliente = @idCliente", oconexion);
                command.Parameters.Add("@idCliente", SqlDbType.Int).Value = id;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    imageData = (byte[])reader["rutaFoto"];
                }
            }

            return File(imageData, "image/jpeg");
        }

    }
}
