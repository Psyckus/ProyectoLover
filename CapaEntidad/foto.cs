using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class foto
    {
        public int idfoto { get; set; }
        public string rutaFoto { get; set; }
        public cliente ocliente { get; set; }
        public string base64 { get; set; }
        public string extencion { get; set; }


        public static foto FromStream(MemoryStream ms)
        {
            foto fotos = new foto();
            byte[] imagenBytes = ms.ToArray();
            string base64 = Convert.ToBase64String(imagenBytes);
            string extension = Path.GetExtension(fotos.rutaFoto);

            // Crear una instancia de foto con los datos necesarios
            var foto = new foto
            {
                base64 = base64,
                extencion = extension
            };

            return foto;
        }


    }
}
