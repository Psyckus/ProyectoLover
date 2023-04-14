using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Preferencia
    {
        private CD_Preferencias objCapaDato = new CD_Preferencias();

        public List<preferencia> Listar()
        {
            return objCapaDato.Listar();
        }
        public List<categoria_interes> ListarCaInt()
        {
            return objCapaDato.ListarCaInt();
        }
        public int Registrar(preferencia obj, out string mensaje)
        {
            mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "Debe ingresar un nombre";
            }

            else if (obj.oCategoria_interes.idCategoria_interes == 0)
            {
                mensaje = "Debe ingresar la categoria";
            }


            return objCapaDato.Registrar(obj, out mensaje);

        }

        public bool editar(preferencia obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "Debe ingresar un nombre";
            }
            else if (obj.oCategoria_interes.idCategoria_interes == 0)
            {
                mensaje = "Debe ingresar la categoria";
            }


            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDato.editar(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }
    }
}
