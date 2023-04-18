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
        public int Registrar(interes obj, out string mensaje)
        {
            mensaje = string.Empty;
            
            if (obj.oCategoria_interes.idCategoria_interes == 0)
            {
                mensaje = "Debe ingresar un tipo de categoria";
            }

            else if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "Debe ingresar un nombre";
            }
            return objCapaDato.Registrar(obj, out mensaje);


          }
           

        

        public bool editar(interes obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.oCategoria_interes.idCategoria_interes == 0)
            {
                mensaje = "Debe ingresar una categoria";
            }

            else if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
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
