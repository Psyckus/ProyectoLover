using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CodigoAutenticacion
    {
        public int id_codigo { get; set; }

        public int IdCliente { get; set; }
        public string Codigo { get; set; }
        public DateTime fecha_creacion { get; set; }
    }
}
