using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_ModEmparejamiento
    {
        private CD_ModEmparejamieto objCapaDato = new CD_ModEmparejamieto();

        public List<ModEmparejamiento> ListaModulo()
        {
            return objCapaDato.ListaModulo();
        }

        public bool editar(ModEmparejamiento obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (obj.CriterioInteres == 0)
            {
                mensaje = "Debe ingresar wl criterio de interes";
            }


            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDato.editarModulo(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

    }
}
