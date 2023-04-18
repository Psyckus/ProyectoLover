using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace capaPresentacionCliente.Controllers
{
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {

            ObtenerIntereses();
            ObtenerFotosCliente();
            ObtenerCategorias();
            return View();

        }


        [HttpPost]
        public JsonResult ObtenerInteresesPorCategoria(List<int> categorias)
        {
            List<interes> intereses = new List<interes>();
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                string query = "SELECT idinteres, idCategoria_interes, nombre FROM interes WHERE estado = 1 AND idCategoria_interes IN (" + string.Join(",", categorias) + ")";
                SqlCommand cmd = new SqlCommand(query, oconexion);
                oconexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    interes interes = new interes();
                    interes.idinteres = Convert.ToInt32(reader["idinteres"]);
                    interes.idCategoria_interes = Convert.ToInt32(reader["idCategoria_interes"]);
                    interes.nombre = reader["nombre"].ToString();
                    intereses.Add(interes);
                }
            }

            return Json(intereses);
        }
        public void ObtenerCategorias()
        {
            List<categoria_interes> categorias = new List<categoria_interes>();
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT idCategoria_interes, nombre FROM categoria_interes WHERE estado = 1", oconexion);
                oconexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categoria_interes categoria = new categoria_interes();
                    categoria.idCategoria_interes = Convert.ToInt32(reader["idCategoria_interes"]);
                    categoria.nombre = reader["nombre"].ToString();
                    categorias.Add(categoria);
                }
            }
            ViewBag.Categorias = categorias;

        }

        public void ObtenerFotosCliente()
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;

            var idCliente = oClienteSession.idCliente;
            List<foto> fotos = new List<foto>();
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                string query = "SELECT idfoto,rutaFoto FROM foto WHERE idCliente = @idCliente";
                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                oconexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int idfoto = Convert.ToInt32(reader["idfoto"]);

                    byte[] imagenBytes = (byte[])reader["rutaFoto"];
                    using (MemoryStream ms = new MemoryStream(imagenBytes))
                    {
                        foto f = foto.FromStream(ms);
                        f.idfoto = idfoto;
                        fotos.Add(f);
                    }
                }
            }
            ViewBag.fotos = fotos;
        }

        public ActionResult EliminarFoto(int id)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                string query = "DELETE FROM foto WHERE idfoto = @id";
                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.Parameters.AddWithValue("@id", id);
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index"); // redirige a la página principal de fotos después de eliminar la foto
        }
        public ActionResult SubirImagen(HttpPostedFileBase file)
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;

            var idCliente = oClienteSession.idCliente;

            string mensaje = string.Empty;

            bool respuesta = new CN_Clientes().Pregunta3(idCliente, file, out mensaje);
            if (respuesta)
            {

                TempData["idCliente"] = idCliente;
                TempData["Message"] = "La imagen se ha cargado correctamente.";
                TempData["MessageType"] = "success";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["idCliente"] = idCliente;
                TempData["Message"] = "Hubo un problema al cargar la imagen.";
                TempData["MessageType"] = "error";
                return View("Index");
            }

        }

        [HttpGet]
        public ActionResult GetImagePrincipal(int id)
        
        {
            byte[] imageData = null;


            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                oconexion.Open();

                SqlCommand command = new SqlCommand("SELECT rutaFoto FROM foto WHERE idCliente = @idCliente and esPrincipal = 1", oconexion);
                command.Parameters.Add("@idCliente", SqlDbType.Int).Value = id;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    imageData = (byte[])reader["rutaFoto"];
                }
            }

            if (imageData == null)
            {
                return View();
            }
            else
            {
                return File(imageData, "image/jpeg");
            }

        }
     
        [HttpPost]
        public ActionResult EstablecerComoPrincipal(int idfoto)
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;

            var idCliente = oClienteSession.idCliente;
            string error = null;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    oconexion.Open();
                    SqlCommand command = new SqlCommand("UPDATE foto SET esPrincipal = CASE WHEN idFoto = @idfoto THEN 1 ELSE 0 END WHERE idCliente = @idCliente", oconexion);
                    command.Parameters.Add("@idfoto", SqlDbType.Int).Value = idfoto;
                    command.Parameters.Add("@idCliente", SqlDbType.Int).Value = idCliente;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier error que pueda ocurrir al actualizar la base de datos
                error = ex.Message;
            }

            if (error != null)
            {
                ViewBag.Error = error;
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public JsonResult CargarDatosCliente()
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;

            var idCliente = oClienteSession.idCliente;

            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                oconexion.Open();
                SqlCommand command = new SqlCommand("SELECT nombre , descripcion  FROM cliente WHERE idCliente = @idCliente", oconexion);
                command.Parameters.Add("@idCliente", SqlDbType.Int).Value = idCliente;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string nombre = (string)reader["nombre"];
                    string descripcion = (string)reader["descripcion"];

                    // Convertir los datos en formato JSON
                    var datos = new { Nombre = nombre, Descripcion = descripcion };
                    return Json(datos, JsonRequestBehavior.AllowGet);
                }
            }


            return Json(null, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ActualizarDatosCliente(string description)
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;

            var idCliente = oClienteSession.idCliente;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    oconexion.Open();
                    SqlCommand command = new SqlCommand("UPDATE cliente SET  descripcion = @descripcion WHERE idCliente = @idCliente", oconexion);
                    command.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = description;
                    command.Parameters.Add("@idCliente", SqlDbType.Int).Value = idCliente;
                    command.ExecuteNonQuery();
                }
                return Json(new { success = true, message = "Datos actualizados correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar los datos: " + ex.Message });
            }
        }
        public void ObtenerIntereses()
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;

            var idCliente = oClienteSession.idCliente;
            List<interes> intereses = new List<interes>();
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT i.idinteres, i.nombre FROM interes i INNER JOIN interes_cliente ic ON i.idinteres = ic.idinteres INNER JOIN cliente c ON ic.idCliente = c.idCliente WHERE c.idCliente = @idCliente", oconexion);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                oconexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    interes interes = new interes();
                    interes.idinteres = Convert.ToInt32(reader["idinteres"]);
                    interes.nombre = reader["nombre"].ToString();
                    intereses.Add(interes);
                }
            }
            ViewBag.Intereses = intereses;
        }


        public JsonResult eliminarInteres(int idInteres)
        {

            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;

            var idCliente = oClienteSession.idCliente;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cn))
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM interes_cliente WHERE idInteres = @idInteres AND idCliente = @idCliente", oconexion);
                    cmd.Parameters.AddWithValue("@idInteres", idInteres);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.ExecuteNonQuery();
                }
                return Json(new { success = true, message = "Datos borrados  correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar los datos: " + ex.Message });
            }
        }


        [HttpGet]
        public JsonResult ObtenerDatosInteres()
        {
            string queryStringMax = "SELECT LimiteSeleccion FROM Configuracion where id_configuracion = 9";
            int maxIntereses = 0;

            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                SqlCommand command = new SqlCommand(queryStringMax, oconexion);
                oconexion.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    maxIntereses = (int)reader["LimiteSeleccion"];
                }
                reader.Close();
            }

            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;

            var idCliente = oClienteSession.idCliente;
            string queryStringCount = "SELECT COUNT(*) AS numIntereses FROM interes_cliente WHERE idCliente = @idCliente";
            int numIntereses = 0;

            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                SqlCommand command = new SqlCommand(queryStringCount, oconexion);
                command.Parameters.AddWithValue("@idCliente", idCliente);
                oconexion.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    numIntereses = (int)reader["numIntereses"];
                }
                reader.Close();
            }

            int interesesDisponibles = maxIntereses - numIntereses;

            var viewModel = new
            {
                MaxIntereses = maxIntereses,
                NumIntereses = numIntereses,
                InteresesDisponibles = interesesDisponibles
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult AgregarInteres(string idInteres)
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;

            var idCliente = oClienteSession.idCliente;

            // Convertir la cadena de IDs de intereses en un arreglo de enteros


            string[] interesesIds = idInteres.Split(',');
            int[] interesesIdsInt = Array.ConvertAll(interesesIds, int.Parse);

            //aquí debes insertar los intereses en la base de datos con el ID del cliente
            foreach (int interesId in interesesIdsInt)
            {
                string query = "INSERT INTO interes_cliente (idinteres,idCliente) VALUES (@idinteres,@idCliente)";
                using (SqlConnection conn = new SqlConnection(conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idinteres", interesId);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return Json(new { success = true });
        }
    }
}