using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;
namespace CapaNegocios
{
    public class CN_Categoria
    {
        private CD_Categoria objCapaDato = new CD_Categoria();

        public List<categoria_interes> Listar()
        {
            return objCapaDato.Listar();
        }
        public int Registrar(categoria_interes obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "Debe ingresar un nombre";
            }

            return objCapaDato.Registrar(obj, out mensaje);

        }



        public bool editar(categoria_interes obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "Debe ingresar un nombre";
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

