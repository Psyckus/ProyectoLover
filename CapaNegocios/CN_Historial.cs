using CapaEntidad;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Historial
    {
        private CD_Historial objCapaDato = new CD_Historial();

        public List<cliente> ObtenerClientesGustados(int idCliente)
        {
            return objCapaDato.ObtenerClientesGustados(idCliente);
        }
        public List<cliente> ObtenerClientesQuienMeGusta(int idCliente)
        {
            return objCapaDato.ObtenerClientesQuienMeGusta(idCliente);
        }
        public List<cliente> ObtenerClientesYaNoMeGusta(int idCliente)
        {
            return objCapaDato.ObtenerClientesYaNoMeGusta(idCliente);
        }

    }
}
