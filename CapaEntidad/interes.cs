using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class interes 
    {
        public int idinteres { get; set; }

        public string nombreI { get; set; }
        public categoria_interes oCategoria_interes { get; set; }
        public bool estado { get; set; }
    }
}
