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
        // GET: TestPersonalidad
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





    }
}