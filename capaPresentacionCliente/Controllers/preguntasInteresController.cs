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
        public ActionResult Pregunta4()

        {
            ObtenerCategorias();
            return View();
        }
        public ActionResult Pregunta5()

        {
            ObtenerGeneros();

            return View();
        }
        public void ObtenerGeneros()
        {
            // Obtener todos los intereses activos
            List<tipo_genero> tipo_generos = new List<tipo_genero>();
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT idtipoGenero , nombre  FROM tipo_genero ", oconexion);
                oconexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tipo_genero tipo_genero = new tipo_genero();
                    tipo_genero.idtipoGenero = Convert.ToInt32(reader["idtipoGenero"]);
                    tipo_genero.nombre = reader["nombre"].ToString();
                    tipo_generos.Add(tipo_genero);
                }
            }
            ViewBag.tipo_generos = tipo_generos;

        }
        [HttpPost]
        public ActionResult Pregunta5(int generos)
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;
            var idCliente = oClienteSession.idCliente;

            //Guardar el género seleccionado en la base de datos
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO generoCliente (idtipoGenero, idCliente) VALUES (@idtipoGenero,@idCliente)", oconexion);
                cmd.Parameters.AddWithValue("@idtipoGenero", generos);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }

            // Redirigir al usuario a la siguiente página
            return RedirectToAction("DobleAutenticacion", "Aceeso");




        }

        [HttpPost]
        public ActionResult Pregunta1( string fecha_nac)
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;



            var idCliente = oClienteSession.idCliente;
            cliente oCliente = new cliente();
            oCliente = new CN_Clientes().Listar().Where(u => u.idCliente == idCliente).FirstOrDefault();
            string mensaje = string.Empty;
            bool respuesta = new CN_Clientes().Pregunta1(idCliente, fecha_nac, out mensaje);

            if (respuesta)
            {
              
                TempData["idCliente"] = idCliente;
                return RedirectToAction("Pregunta3");
            }
            else
            {
                TempData["idCliente"] = idCliente;
                ViewBag.Error = mensaje;
                return View();
            }

        }
        [HttpPost]
        public ActionResult Pregunta2( string descripcion)
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;



            var idCliente = oClienteSession.idCliente;
            string mensaje = string.Empty;
            bool respuesta = new CN_Clientes().Pregunta2(idCliente, descripcion, out mensaje);
            if (respuesta)
            {



                return RedirectToAction("Pregunta5");
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
        public void ObtenerIntereses()
        {
            // Obtener todos los intereses activos
            List<interes> intereses = new List<interes>();
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT idinteres, idCategoria_interes, nombre FROM interes WHERE estado = 1", oconexion);
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
            ViewBag.Intereses = intereses;

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
        [HttpPost]

        public ActionResult Pregunta4(string interesesSeleccionados)
        {
            var session = HttpContext.Session;
            var oClienteSession = session["Cliente"] as CapaEntidad.cliente;



            var idCliente = oClienteSession.idCliente;
            string[] interesesIds = interesesSeleccionados.Split(',');
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
            TempData["idCliente"] = idCliente;
            return RedirectToAction("Pregunta1");


        }
        public int ObtenerLimiteMaximoIntereses()
        {
            int limiteMaximoIntereses = 0;
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                SqlCommand cmd = new SqlCommand("SELECT LimiteSeleccion FROM configuracion WHERE Nombre = 'LimiteMaximoIntereses'", oconexion);
                oconexion.Open();
                var resultado = cmd.ExecuteScalar();
                if (resultado != null)
                {
                    limiteMaximoIntereses = Int32.Parse(resultado.ToString());
                }
            }
            return limiteMaximoIntereses;
        }
        [HttpPost]
        public ActionResult Pregunta3(int idCliente, HttpPostedFileBase file)
        {
            cliente oCliente = new cliente();
            oCliente = new CN_Clientes().Listar().Where(u => u.idCliente == idCliente).FirstOrDefault();
            string mensaje = string.Empty;
            bool respuesta = new CN_Clientes().Pregunta3(idCliente, file, out mensaje);
            if (respuesta)
            {
                var session = HttpContext.Session;
                var oClienteSession = session["Cliente"];
                TempData["idCliente"] = idCliente;
                TempData["Message"] = "La imagen se ha cargado correctamente.";
                TempData["MessageType"] = "success";
                //coregir aqui
                return RedirectToAction("Pregunta4");

            }
            else
            {
                TempData["idCliente"] = idCliente;
                TempData["Message"] = "Hubo un problema al cargar la imagen.";
                TempData["MessageType"] = "error";
                //ViewBag.Error = mensaje;
                return View();
            }

        }

        [HttpPost]
        public JsonResult ObtenerInteresesPorCliente(int id)
        {
            List<interes_cliente> lista = new List<interes_cliente>();
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                oconexion.Open();



                string sql = "select i.nombre from interes as i join interes_cliente as ic on i.idInteres = ic.idInteres and ic.idCliente = @id";



                SqlCommand cmd = new SqlCommand(sql, oconexion);
                cmd.Parameters.AddWithValue("@id", id);



                SqlDataReader reader = cmd.ExecuteReader();



                while (reader.Read())
                {
                    interes_cliente interes = new interes_cliente();
                    interes.oInteres = reader["nombre"].ToString();
                    lista.Add(interes);
                }
            }



            return Json(lista);
        }

        [HttpPost]
        public JsonResult ObtenerPreferenciasPorCliente(int id)
        {
            List<preferencia_cliente> lista = new List<preferencia_cliente>();
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                oconexion.Open();



                string sql = "select p.nombre from preferencia as p join preferencia_cliente as ic on p.idPreferencia = ic.idPreferencia and ic.idCliente = @id";



                SqlCommand cmd = new SqlCommand(sql, oconexion);
                cmd.Parameters.AddWithValue("@id", id);



                SqlDataReader reader = cmd.ExecuteReader();



                while (reader.Read())
                {
                    preferencia_cliente preferencia = new preferencia_cliente();
                    preferencia.oPreferencia = reader["nombre"].ToString();
                    lista.Add(preferencia);
                }
            }



            return Json(lista);
        }
        [HttpPost]
        public JsonResult ObtenerGeneroPorCliente(int id)
        {
            List<generoCliente> lista = new List<generoCliente>();
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                oconexion.Open();



                string sql = "select t.nombre from tipo_genero as t join generoCliente as g on t.idtipoGenero= g.idtipoGenero and g.idCliente = @id";



                SqlCommand cmd = new SqlCommand(sql, oconexion);
                cmd.Parameters.AddWithValue("@id", id);



                SqlDataReader reader = cmd.ExecuteReader();



                while (reader.Read())
                {
                    generoCliente genero = new generoCliente();
                    genero.ocliente = reader["nombre"].ToString();
                    lista.Add(genero);
                }
            }



            return Json(lista);
        }
    }
}
