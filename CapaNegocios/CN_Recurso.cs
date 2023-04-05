using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CapaEntidad;
using System.Data.SqlClient;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Recurso
    {
        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool restultado = false;

            try
            {
                //Objeto de mail
                MailMessage mail = new MailMessage();
                //Correo a donde se va a enviar 
                mail.To.Add(correo);
                //De donde se va a enviar
                mail.From = new MailAddress("euforia806@gmail.com");
                //El asunto del mensaje
                mail.Subject = asunto;
                //El mensaje que se va a enviar
                mail.Body = mensaje;
                //Se indica que es html
                mail.IsBodyHtml = true;

                //Una variable de tipo servidor que se encarga de enviar al mensaje 
                var smtp = new SmtpClient()
                {
                    //Se agrega nuestro correo y contraseña
                    Credentials = new NetworkCredential("euforia806@gmail.com", "vftbfkryqppotffb"),
                    //El srvidor que utiliza gmail para los correos 
                    Host = "smtp.gmail.com",
                    //  El puerto por el que se envian 
                    Port = 587,
                    //Certificado de seguridad 
                    EnableSsl = true
                };
                //Para que envie el coreo
                smtp.Send(mail);

                //Resultado pasa a ser true
                restultado = true;




            }
            catch (Exception)
            {
                restultado = false;

            }

            return restultado;
        }


        public static string ConvertirBase64(string ruta, out bool conversion)
        {
            string textoBase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textoBase64 = Convert.ToBase64String(bytes);

            }
            catch
            {

                conversion = false;
            }

            return textoBase64;

        }
        public static string GenerarCodigo()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static async Task<bool> EnviarCorreoAsync(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                //Objeto de mail
                MailMessage mail = new MailMessage();
                //Correo a donde se va a enviar 
                mail.To.Add(correo);
                //De donde se va a enviar
                mail.From = new MailAddress("euforia806@gmail.com");
                //El asunto del mensaje
                mail.Subject = asunto;
                //El mensaje que se va a enviar
                mail.Body = mensaje;
                //Se indica que es html
                mail.IsBodyHtml = true;

                //Una variable de tipo servidor que se encarga de enviar al mensaje 
                var smtp = new SmtpClient()
                {
                    //Se agrega nuestro correo y contraseña
                    Credentials = new NetworkCredential("euforia806@gmail.com", "vftbfkryqppotffb"),
                    //El srvidor que utiliza gmail para los correos 
                    Host = "smtp.gmail.com",
                    //  El puerto por el que se envian 
                    Port = 587,
                    //Certificado de seguridad 
                    EnableSsl = true
                };

                // Para enviar el correo de manera asincrónica
                await smtp.SendMailAsync(mail);

                // Resultado pasa a ser true
                resultado = true;
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

        public static async Task<bool> ValidarCodigoAsync(int idCliente, string codigo)
        {
            // Buscar el último registro de código de autenticación para el cliente
            CodigoAutenticacion codigoAutenticacion = null;
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                var query = "SELECT TOP 1 * FROM codigo_autenticacion WHERE idcliente=@idCliente ORDER BY fecha_creacion DESC";
                using (var cmd = new SqlCommand(query, oconexion))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    await oconexion.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            codigoAutenticacion = new CodigoAutenticacion
                            {
                                id_codigo = (int)reader["id_codigo"],
                                IdCliente = (int)reader["idcliente"],
                                Codigo = (string)reader["codigo"],
                                fecha_creacion = (DateTime)reader["fecha_creacion"]
                            };
                        }
                    }
                }
            }

            // Si no se encontró ningún registro, el código no es válido
            if (codigoAutenticacion == null)
            {
                return false;
            }

            // Verificar que el código sea correcto
            if (codigo != codigoAutenticacion.Codigo)
            {
                return false;
            }

            // Verificar que el código no haya expirado (5 minutos)
            if (DateTime.Now.Subtract(codigoAutenticacion.fecha_creacion).TotalMinutes > 5)
            {
                return false;
            }

            // Eliminar el registro de código de autenticación
            using (SqlConnection oconexion = new SqlConnection(conexion.cn))
            {
                var query = "DELETE FROM codigo_autenticacion WHERE id_codigo=@id_codigo";
                using (var cmd = new SqlCommand(query, oconexion))
                {
                    cmd.Parameters.AddWithValue("@id_codigo", codigoAutenticacion.id_codigo);
                    await oconexion.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            // Si se llegó hasta aquí, el código es válido
            return true;
        }

    }
}
