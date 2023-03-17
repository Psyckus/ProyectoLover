using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
namespace CapaNegocios
{
    public class CN_Interes
    {
        private CD_Interes objCapaDato = new CD_Interes();

        public List<interes> Listar()
        {
            return objCapaDato.Listar();
        }
        public List<categoria_interes> ListarCaI()
        {
            return objCapaDato.ListarCaI();
        }
    }
}
