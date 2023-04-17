using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class preferencia
    {
        public int idPreferencia { get; set; }
        public string nombre { get; set; }
        public bool estado { get; set; }
        public categoria_interes oCategoria_interes { get; set; }
    }
}
