using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using capaPresentacionCliente.Modelo;

namespace capaPresentacionCliente.Controllers
{
    public class chatController : Controller
    {
        private readonly string connectionString = "Data Source=tiusr3pl.cuc-carrera-ti.ac.cr/MSSQLSERVER2019;Initial Catalog=tiusr3pl_lover; user=loverusr; password=lovergrupo5; Integrated Security=True";
        // GET: Home
        public ActionResult Index()
        {
            List<Mensajes> mensajes = ObtenerMensajes();
            return View(mensajes);
        }
        [HttpPost]
        public ActionResult Enviar(string idCliente2, string Mensaje)
        {
            string idCliente1 = User.Identity.Name;
            EnviarMensaje(idCliente1, idCliente2, Mensaje);
            return RedirectToAction("Index");
        }

        private void EnviarMensaje(string idCliente1, string idCliente2, string Mensaje)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Mensajes (idCliente1, idCliente2, Mensaje, FechaHora) VALUES (@idCliente1, @idCliente2, @Mensaje, @FechaHora)", connection);
                command.Parameters.AddWithValue("@idCliente1", idCliente1);
                command.Parameters.AddWithValue("@idCliente2", idCliente2);
                command.Parameters.AddWithValue("@Mensaje", Mensaje);
                command.Parameters.AddWithValue("@FechaHora", DateTime.Now);
                command.ExecuteNonQuery();
            }
        }

        private List<Mensajes> ObtenerMensajes()
        {
            List<Mensajes> mensajes = new List<Mensajes>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Mensajes WHERE idCliente1 = @Usuario OR idCliente2 = @Usuario ORDER BY FechaHora", connection);
                command.Parameters.AddWithValue("@Usuario", User.Identity.Name);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Mensajes mensaje = new Mensajes
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        idCliente1 = Convert.ToInt32(reader["idCliente1"]),
                        idCliente2 = Convert.ToInt32(reader["idCliente2"]),
                        Mensaje = reader["Mensaje"].ToString(),
                        FechaHora = Convert.ToDateTime(reader["FechaHora"])
                    };
                    mensajes.Add(mensaje);
                }
            }
            return mensajes;


        }
    }

}