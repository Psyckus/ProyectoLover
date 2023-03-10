using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

    }
}
