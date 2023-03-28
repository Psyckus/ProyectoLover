using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using capaPresentacionCliente.Modelo;
using System.Threading.Tasks;

namespace capaPresentacionCliente.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string connectionString = "Data Source=tiusr3pl.cuc-carrera-ti.ac.cr/MSSQLSERVER2019;Initial Catalog=tiusr3pl_lover; user=loverusr; password=lovergrupo5; Integrated Security=True";

        public void EnviarMensaje(string idCliente1, string idCliente2, string mensaje)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Mensajes (idCliente1, idCliente2, Mensaje, FechaHora) VALUES (@idCliente1, @idCliente2, @Mensaje, @FechaHora)", connection);
                command.Parameters.AddWithValue("@idCliente1", idCliente1);
                command.Parameters.AddWithValue("@idCliente2", idCliente2);
                command.Parameters.AddWithValue("@Mensaje", mensaje);
                command.Parameters.AddWithValue("@FechaHora", DateTime.Now);
                command.ExecuteNonQuery();
            }
        }

       // private void RecibirMensaje(Mensajes mensaje)
       // {
         //   Clients.Group(mensaje.idCliente1).SendAsync("RecibirMensaje", mensaje);
       // }

        //public override async Task OnConnectedAsync()
        //{
          //  await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
          //  await base.OnConnectedAsync();
       //}

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
         //   await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
         //   await base.OnDisconnectedAsync(exception);
        //}
    }
}
//}