using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class cliente
    {
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public DateTime fecha_nac { get; set; }
        public string descripcion { get; set; }
        public string clave { get; set; }
        public string ConfirmarClave { get; set; }
        public bool Reestablecer { get; set; }
        public bool activo { get; set; }

        public bool itsActive { get; set; }

    }
}
