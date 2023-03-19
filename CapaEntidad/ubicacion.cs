using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ubicacion
    {
        public int idUbicacion { get; set; }
        public int idCliente { get; set; }
        public string latitud { get; set; }
        public string logitud { get; set;}
    }
}
