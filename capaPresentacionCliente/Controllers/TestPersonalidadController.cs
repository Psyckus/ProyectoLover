using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace capaPresentacionCliente.Controllers
{
    public class TestPersonalidadController : Controller
    {
        public ActionResult Index()
        {
            ObtenerDescripciones();
            return View();
        }



        public void ObtenerDescripciones()
        {
            // Crear la conexión a la base de datos
            SqlConnection conn = new SqlConnection(conexion.cn);
            conn.Open();

            // Crear el comando SQL
            string query = "SELECT TOP 3 descripcion FROM test";
            SqlCommand cmd = new SqlCommand(query, conn);

            // Ejecutar el comando y leer los resultados
            SqlDataReader reader = cmd.ExecuteReader();

            // Crear dos variables para almacenar las descripciones
            string descripcion1 = "";
            string descripcion2 = "";
            string descripcion3 = "";

            if (reader.Read())
            {
                // Guardar la descripción del primer registro en la variable "descripcion1"
                descripcion1 = reader["descripcion"].ToString();
            }

            if (reader.Read())
            {
                // Guardar la descripción del segundo registro en la variable "descripcion2"
                descripcion2 = reader["descripcion"].ToString();
            }
            if (reader.Read())
            {
                // Guardar la descripción del segundo registro en la variable "descripcion2"
                descripcion3 = reader["descripcion"].ToString();
            }

            // Cerrar la conexión a la base de datos
            conn.Close();

            // Pasar las descripciones a la vista como variables
            ViewBag.Descripcion1 = descripcion1;
            ViewBag.Descripcion2 = descripcion2;
            ViewBag.Descripcion3 = descripcion3;


        }
        [HttpGet]
        public JsonResult ObtenerPreguntas1()
        {
            List<object> preguntas = new List<object>();

            // Crear la conexión a la base de datos
            using (SqlConnection conn = new SqlConnection(conexion.cn))
            {
                conn.Open();

                // Crear el comando SQL para obtener las preguntas
                string query = "SELECT * FROM preguntaTest WHERE idTest = (SELECT idtest FROM test WHERE nombre = 'Test estrella polar')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Crear la pregunta actual
                        int idPregunta = (int)reader["idPreguntaTest"];
                        string textoPregunta = reader["nombre"].ToString();
                        List<object> opciones = new List<object>();

                        // Crear el comando SQL para obtener las respuestas de la pregunta actual
                        string respuestaQuery = "SELECT * FROM respuestaTest WHERE idPregunta = @idPregunta";
                        using (SqlCommand respuestaCmd = new SqlCommand(respuestaQuery, conn))
                        {
                            respuestaCmd.Parameters.AddWithValue("@idPregunta", idPregunta);
                            SqlDataReader respuestaReader = respuestaCmd.ExecuteReader();

                            while (respuestaReader.Read())
                            {
                                // Crear la opción actual
                                int idRespuesta = (int)respuestaReader["id_respuestaTest"];
                                string textoRespuesta = respuestaReader["respuesta"].ToString();
                                object opcion = new { id = idRespuesta, texto = textoRespuesta };

                                // Agregar la opción a la lista de opciones
                                opciones.Add(opcion);
                            }

                            // Cerrar el DataReader de las respuestas
                            respuestaReader.Close();
                        }

                        // Crear la pregunta como un objeto anónimo
                        object pregunta = new { id = idPregunta, texto = textoPregunta, opciones = opciones };

                        // Agregar la pregunta a la lista de preguntas
                        preguntas.Add(pregunta);
                    }

                    // Cerrar el DataReader de las preguntas
                    reader.Close();
                }
            }

            // Devolver las preguntas como un objeto JSON
            return Json(preguntas, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult ObtenerPreguntas2()
        {
            List<object> preguntas = new List<object>();

            // Crear la conexión a la base de datos
            using (SqlConnection conn = new SqlConnection(conexion.cn))
            {
                conn.Open();

                // Crear el comando SQL para obtener las preguntas
                string query = "SELECT * FROM preguntaTest WHERE idTest = (SELECT idtest FROM test WHERE nombre = 'Test de psicología')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Crear la pregunta actual
                        int idPregunta = (int)reader["idPreguntaTest"];
                        string textoPregunta = reader["nombre"].ToString();
                        List<object> opciones = new List<object>();

                        // Crear el comando SQL para obtener las respuestas de la pregunta actual
                        string respuestaQuery = "SELECT * FROM respuestaTest WHERE idPregunta = @idPregunta";
                        using (SqlCommand respuestaCmd = new SqlCommand(respuestaQuery, conn))
                        {
                            respuestaCmd.Parameters.AddWithValue("@idPregunta", idPregunta);
                            SqlDataReader respuestaReader = respuestaCmd.ExecuteReader();

                            while (respuestaReader.Read())
                            {
                                // Crear la opción actual
                                int idRespuesta = (int)respuestaReader["id_respuestaTest"];
                                string textoRespuesta = respuestaReader["respuesta"].ToString();
                                object opcion = new { id = idRespuesta, texto = textoRespuesta };

                                // Agregar la opción a la lista de opciones
                                opciones.Add(opcion);
                            }

                            // Cerrar el DataReader de las respuestas
                            respuestaReader.Close();
                        }

                        // Crear la pregunta como un objeto anónimo
                        object pregunta = new { id = idPregunta, texto = textoPregunta, opciones = opciones };

                        // Agregar la pregunta a la lista de preguntas
                        preguntas.Add(pregunta);
                    }

                    // Cerrar el DataReader de las preguntas
                    reader.Close();
                }
            }

            // Devolver las preguntas como un objeto JSON
            return Json(preguntas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerPreguntas3()
        {
            List<object> preguntas = new List<object>();

            // Crear la conexión a la base de datos
            using (SqlConnection conn = new SqlConnection(conexion.cn))
            {
                conn.Open();

                // Crear el comando SQL para obtener las preguntas
                string query = "SELECT * FROM preguntaTest WHERE idTest = (SELECT idtest FROM test WHERE nombre = 'Test de ansiedad')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Crear la pregunta actual
                        int idPregunta = (int)reader["idPreguntaTest"];
                        string textoPregunta = reader["nombre"].ToString();
                        List<object> opciones = new List<object>();

                        // Crear el comando SQL para obtener las respuestas de la pregunta actual
                        string respuestaQuery = "SELECT * FROM respuestaTest WHERE idPregunta = @idPregunta";
                        using (SqlCommand respuestaCmd = new SqlCommand(respuestaQuery, conn))
                        {
                            respuestaCmd.Parameters.AddWithValue("@idPregunta", idPregunta);
                            SqlDataReader respuestaReader = respuestaCmd.ExecuteReader();

                            while (respuestaReader.Read())
                            {
                                // Crear la opción actual
                                int idRespuesta = (int)respuestaReader["id_respuestaTest"];
                                string textoRespuesta = respuestaReader["respuesta"].ToString();
                                object opcion = new { id = idRespuesta, texto = textoRespuesta };

                                // Agregar la opción a la lista de opciones
                                opciones.Add(opcion);
                            }

                            // Cerrar el DataReader de las respuestas
                            respuestaReader.Close();
                        }

                        // Crear la pregunta como un objeto anónimo
                        object pregunta = new { id = idPregunta, texto = textoPregunta, opciones = opciones };

                        // Agregar la pregunta a la lista de preguntas
                        preguntas.Add(pregunta);
                    }

                    // Cerrar el DataReader de las preguntas
                    reader.Close();
                }
            }

            // Devolver las preguntas como un objeto JSON
            return Json(preguntas, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TestPersonalidad1()
        {

            return View();
        }


        public ActionResult Test1Preguntas()
        {
         
            return View();
        }
        public ActionResult TestPersonalidad2()
        {

            return View();
        }


        public ActionResult Test2Preguntas()
        {

            return View();
        }

        public ActionResult TestPersonalidad3()
        {

            return View();
        }


        public ActionResult Test3Preguntas()
        {

            return View();
        }
















    }

}